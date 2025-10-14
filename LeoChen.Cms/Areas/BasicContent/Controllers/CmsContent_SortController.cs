using LeoChen.Cms.Data;
using LeoChen.Cms.TemplateEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewLife;
using NewLife;
using NewLife.Caching;
using NewLife.Cube;
using NewLife.Cube.Common;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Web;
using XCode.Membership;
using XCode.Model;
using static LeoChen.Cms.Data.CmsContent_Sort;

namespace LeoChen.Cms.Areas.BasicContent.Controllers;

/// <summary>内容分类表</summary>
[Menu(10, true, Icon = "fa-table")]
[BasicContentArea]
public class CmsContent_SortController : EntityController<CmsContent_Sort>
{
    private readonly ICacheProvider _cacheProvider;
    private readonly TemplateEngineCache _templateEngineCache;

    static CmsContent_SortController()
    {
        ListFields.RemoveCreateField().RemoveRemarkField().RemoveUpdateField();

        {
            var df = AddFormFields.GetField(__.ListTpl) as FormField;
            df.ItemType = "singleSelect";
            df.DataSource = o =>
            {
                var dic = new Dictionary<Int32, String>();
                var site = CmsSite.FindByAreaID(CmsAreaContext.CurrentId);
                if (site == null) return dic;
                return GetTpl(site.Theme);
            };
        }
        {
            var df = EditFormFields.GetField(__.ListTpl) as FormField;
            df.ItemType = "singleSelect";
            df.DataSource = o =>
            {
                var dic = new Dictionary<Int32, String>();
                var site = CmsSite.FindByAreaID(CmsAreaContext.CurrentId);
                if (site == null) return dic;
                return GetTpl(site.Theme);
            };
        }
        {
            var df = AddFormFields.GetField(__.ContentTpl) as FormField;
            df.ItemType = "singleSelect";
            df.DataSource = o =>
            {
                var dic = new Dictionary<Int32, String>();
                var site = CmsSite.FindByAreaID(CmsAreaContext.CurrentId);
                if (site == null) return dic;
                return GetTpl(site.Theme);
            };
        }
        {
            var df = EditFormFields.GetField(__.ContentTpl) as FormField;
            df.ItemType = "singleSelect";
            df.DataSource = o =>
            {
                var dic = new Dictionary<Int32, String>();
                var site = CmsSite.FindByAreaID(CmsAreaContext.CurrentId);
                if (site == null) return dic;
                return GetTpl(site.Theme);
            };
        }
        {
            var df = AddFormFields.GetField(__.Pid) as FormField;
            df.AddNull = false;
            df.ItemType = "singleSelect";
            df.DataSource = o =>
            {
                var data = CmsContent_Sort.GetTreeList(CmsAreaContext.CurrentId)
                    .ToDictionary(k=>k.ID,v=>v.TreeNodeText);
                return data;
            };
        }
        
        {
            var df = EditFormFields.GetField(__.Pid) as FormField;
            df.AddNull = false;
            df.ItemType = "singleSelect";
            df.DataSource = o =>
            {
                var data = CmsContent_Sort.GetTreeList(CmsAreaContext.CurrentId)
                    .ToDictionary(k=>k.ID,v=>v.TreeNodeText);
                return data;
            };
        }
    }
    
    protected static Dictionary<String, String> GetTpl(string theme)
    {
        var dic = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
        var tpldir = $"Template/{theme}".GetFullPath().EnsureDirectory(false);
        return tpldir.AsDirectory().GetAllFiles("*.html",false).ToDictionary(k => k.Name, v => v.Name, StringComparer.OrdinalIgnoreCase);
    }
    protected override WhereBuilder CreateWhere()
    {
        HttpContext.Items["AreaID"] = CmsAreaContext.CurrentId;
        return base.CreateWhere();
    }
    
    public override ActionResult Index(Pager p = null)
    {
        PageSetting.EnableTableDoubleClick = false;
        PageSetting.EnableKey = false;
        PageSetting.EnableAdd = true;
        var areaid = CmsAreaContext.CurrentId;

        var list = GetTree();
        // 用于显示的列
        ViewBag.Fields = OnGetFields(ViewKinds.List, list);
        ViewBag.SearchFields = OnGetFields(ViewKinds.Search, list);
        if (IsJsonRequest) return Json(0, null, list, new { page = p });
        return View("ListTree", list);
    }

    /// <summary>
    /// 批量排序
    /// </summary>
    /// <returns></returns>
    public ActionResult AllSorting()
    {
        var idlist = GetRequest("id").SplitAsInt(",");
        var oldsortlist = GetRequest("oldsorting").SplitAsInt(",");
        var sortlist = GetRequest("sorting").SplitAsInt(",");
        if (idlist.Length == oldsortlist.Length && oldsortlist.Length == sortlist.Length)
        {
            var count = 0;
            for (int i = 0; i < idlist.Length; i++)
            {
               
                if (oldsortlist[i] != sortlist[i])
                {
                    count++;
                   var e = FindByID(idlist[i]);
                   e.Sorting = sortlist[i];
                   e.Save();
                }
            }
            return JsonRefresh($"成功排序{count}行");
        }
        
        return JsonRefresh($"请求出错！数据未修改");
    }




    protected override FieldCollection OnGetFields(ViewKinds kind, Object model)
    {
        var rs = base.OnGetFields(kind, model);
        if (kind == ViewKinds.List)
        {
            var keepFieldNames = new List<string> 
            {
                "ID",          // ID
                "Name",        // 名称
                "UrlName",     // Url名称
                "ListTpl",     // 列表模板
                "ContentTpl",  // 内容模板
                "Enable",      // 状态
                "Sorting"      // 排序
            };
            
            var removeFieldNames = rs.Select(f => f.Name).Where(name => !keepFieldNames.Contains(name)).ToArray(); 
            if (removeFieldNames.Length > 0) rs.RemoveField(removeFieldNames); 
        }

        rs.RemoveField("AreaID","AreaName");
        return rs;

    }

    

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsContent_Sort> Search(Pager p)
    {
        var areaId = CmsAreaContext.CurrentId;
        var pid = p["pid"].ToInt(-1);
        var modelId = p["modelId"].ToInt(-1);
        var sorting = p["sorting"].ToInt(-1);
        var urlname = p["urlname"]?.ToString();
        var enable = p["enable"]?.ToBoolean();
        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsContent_Sort.Search(areaId, pid, modelId, urlname,enable,sorting, start, end, p["Q"], p);
    }
    
    
    public CmsContent_SortController(ICacheProvider cacheProvider,TemplateEngineCache templateEngineCache)
    {
        _cacheProvider = cacheProvider;
        _templateEngineCache = templateEngineCache;
    }

    protected override int OnInsert(CmsContent_Sort entity)
    {
        var o = base.OnInsert(entity);
        if (o > 0)
        {
            SetCache(entity);
        }
        return o;
    }

    protected override int OnDelete(CmsContent_Sort entity)
    {       
        var o = base.OnDelete(entity);
        if (o > 0)
        {
            SetCache(entity);
        }
        return o;

    }

    protected override int OnUpdate(CmsContent_Sort entity)
    {
        var o = base.OnUpdate(entity);
        if (o > 0)
        {
            SetCache(entity);
        }
        return o;
    }

    private void SetCache(CmsContent_Sort entity)
    {
        var dic = FindAllByAreaID(entity.AreaID)
            .Where(contentSort => !string.IsNullOrEmpty(contentSort.UrlName))
            .ToDictionary(contentSort => contentSort.ID, contentSort => contentSort);
        _cacheProvider.Cache.Set(TemplateEngineCache.CacheContentSortDic + entity.AreaID, dic, 0);
    }
}