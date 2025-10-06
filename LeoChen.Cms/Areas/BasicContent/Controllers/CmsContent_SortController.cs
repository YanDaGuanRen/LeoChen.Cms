using LeoChen.Cms.Data;
using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife;
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
    static CmsContent_SortController()
    {
        ListFields.RemoveCreateField().RemoveRemarkField().RemoveUpdateField();
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
                "ListTpl",     // 列表模板
                "ContentTpl",  // 内容模板
                "Enable",      // 状态
                "Sorting"      // 排序
            };

            // 3. 计算需要删除的字段名（所有不在keep列表中的字段）
            var removeFieldNames = rs.Select(f => f.Name)
                .Where(name => !keepFieldNames.Contains(name))
                .ToArray(); // 转换为数组，方便传递给RemoveField

            if (removeFieldNames.Length > 0)
            {
                rs.RemoveField(removeFieldNames); 
            }
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
        var enable = p["enable"]?.ToBoolean();

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsContent_Sort.Search(areaId, pid, modelId, enable,sorting, start, end, p["Q"], p);
    }
}