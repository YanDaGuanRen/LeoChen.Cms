using Microsoft.AspNetCore.Mvc;
using LeoChen.Cms.Data;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Serialization;
using NewLife.Web;
using XCode.Membership;
using static LeoChen.Cms.Data.CmsForm;

namespace LeoChen.Cms.Areas.GlobalConfiguration.Controllers;

/// <summary>自定义表单</summary>
[Menu(10, true, Icon = "fa-table")]
[GlobalConfigurationArea]
public class CmsFormController : EntityController<CmsForm>
{
    static CmsFormController()
    {

        ListFields.RemoveCreateField().RemoveRemarkField().RemoveUpdateField();

        {
            var df = ListFields.AddListField( "FormFields", __.Enable,null);
            df.DisplayName = "表单字段";
            df.Url = "/ExpandContent/CmsFormField?FormID={ID}";
            df.GetValue = e =>
            {
                if (e is ICmsForm cmsForm)
                {
                    var n = CmsFormField.FindAllByFormID(cmsForm.ID);
                    
                    return n!=null ?$"字段 ({n.Count})":"字段";
                }
                else
                {
                    return "字段";
                }
            };
        }
        {
            var df = ListFields.AddListField("FormExts",__.Enable ,null);
            df.DisplayName = "表单内容";
            df.Url = "/ExpandContent/CmsExtForm?FormID={ID}";
            df.GetValue = e =>
            {
                if (e is ICmsForm cmsForm)
                {
                    var n = CmsExtForm.FindAllByFormID(cmsForm.ID);
                    
                    return n!=null ?$"内容 ({n.Count})":"内容";
                }
                else
                {
                    return "内容";
                }
            };
        }

    }
    
    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsForm> Search(Pager p)
    {
        var enable = p["enable"]?.ToBoolean();

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsForm.Search(enable, start, end, p["Q"], p);
    }
}