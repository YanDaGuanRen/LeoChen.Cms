using System.Collections;
using LeoChen.Cms.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Serialization;
using NewLife.Web;
using XCode.Membership;
using static LeoChen.Cms.Data.CmsLabel;

namespace LeoChen.Cms.Areas.GlobalConfiguration.Controllers;

/// <summary>定制标签</summary>
[Menu(30, true, Icon = "fa-table")]
[GlobalConfigurationArea]
public class CmsLabelController : EntityController<CmsLabel>
{
    static CmsLabelController()
    {
        //LogOnChange = true;

        ListFields.RemoveCreateField().RemoveRemarkField().RemoveUpdateField();
        {
            var df = ListFields.AddListField("CmsLabelDisplayName", null, "Name");
            df.DisplayName = "调用标签名称";
            df.Text = "ExtLabel_{Name}";
            df.TextAlign = TextAligns.Center;
        }

    }

    protected override int OnUpdate(CmsLabel entity)
    {       
        if (Request.HasFormContentType)
        {
            var aaa = Request.Form;
            var filteredDict = Request.Form.Where(kv => kv.Key.StartsWithIgnoreCase("CmsExp_"))
                .ToDictionary(kv => kv.Key, kv => kv.Value.FirstOrDefault() );
            entity.Value = JsonHelper.ToJson(filteredDict, false);
        }
        return entity.Update();
    }

    protected override FieldCollection OnGetFields(ViewKinds kind, object model)
    {
        var fields = kind switch
        {
            ViewKinds.List => ListFields,
            ViewKinds.Detail => DetailFields,
            ViewKinds.AddForm => AddFormFields,
            ViewKinds.EditForm => EditFormFields,
            ViewKinds.Search => SearchFields,
            _ => ListFields,
        };
        fields = fields.Clone();
        fields.RemoveField(CmsLabel.__.Value);
        // 表单嵌入配置字段
        if ((kind == ViewKinds.EditForm || kind == ViewKinds.Detail) && model is CmsLabel entity)
        {
            var melist = CmsModelExtfield.FindAllByModelID(1);
            IDictionary<string, object?> dic = new Dictionary<string, object?>();
            if (!entity.Value.IsNullOrEmpty())
            {
                dic = entity.Value.DecodeJson();
            }
            foreach (var cmsModelExtfield in melist)
            {
                var df = new FormField();
                df.Name = "CmsExp_" + cmsModelExtfield.Name;
                df.Category = "扩展字段";
                // df.Length = df.Length;
                df.Nullable = true;
                df.PrimaryKey = false;
                df.DisplayName = cmsModelExtfield.DisplayName;
                df.Description = cmsModelExtfield.Description;
                df.ReadOnly = false;
                dic.TryGetValue(df.Name, out var v);
                
                switch (cmsModelExtfield.FieldType)
                {
                    case CmsItemType.单选:
                        df.Type = typeof(string);
                        if (v == null) v = "";
                        fields.Add(df);
                        entity.SetItem(df.Name, v);
                        break;
                    
                    case CmsItemType.多选:
                        df.Type = typeof(string);
                        if (v == null) v = "";
                        fields.Add(df);
                        entity.SetItem(df.Name, v);
                        break;
                    
                    case CmsItemType.时间:
                        df.Type = typeof(DateTime);
                        if (v == null) v = default(DateTime);
                        fields.Add(df);
                        entity.SetItem(df.Name, v.ToDateTime());
                        break;

                    case CmsItemType.开关:
                        df.Type = typeof(Boolean);
                        if (v == null) v = true;
                        fields.Add(df);
                        entity.SetItem(df.Name, v.ToBoolean());
                        break;
                    case CmsItemType.附件:

                    case CmsItemType.图片:

                    case CmsItemType.单行文本:

                    case CmsItemType.多行文本:

                    case CmsItemType.编辑器:
                        
                    default:
                        df.Type = typeof(string);
                        if (v == null) v = "";
                        fields.Add(df);
                        entity.SetItem(df.Name, v.ToString());
                        break;
                }
            }
        }

        return fields;
    }
    
    //private readonly ITracer _tracer;

    //public CmsLabelController(ITracer tracer)
    //{
    //    _tracer = tracer;
    //}

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsLabel> Search(Pager p)
    {
        var labelType = (LeoChen.Cms.Data.CmsItemType)p["labelType"].ToInt(-1);

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();
        var enable = p["Enable"].ToBoolean();
        return CmsLabel.Search(enable, labelType, start, end, p["Q"], p);
    }
}