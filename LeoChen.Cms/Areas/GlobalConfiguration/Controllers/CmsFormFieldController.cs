using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LeoChen.Cms.Data;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Web;
using XCode.Membership;
using static LeoChen.Cms.Data.CmsFormField;

namespace LeoChen.Cms.Areas.GlobalConfiguration.Controllers;

/// <summary>自定义表单字段</summary>
[Menu(-1, false, Icon = "fa-table")]
[GlobalConfigurationArea]
public class CmsFormFieldController : EntityController<CmsFormField>
{
    static CmsFormFieldController()
    {
        //LogOnChange = true;

        //ListFields.RemoveField("Id", "Creator");
        ListFields.RemoveCreateField().RemoveRemarkField().RemoveUpdateField();
        
        //{
        //    var df = ListFields.GetField("Code") as ListField;
        //    df.Url = "?code={Code}";
        //    df.Target = "_blank";
        //}
        //{
        //    var df = ListFields.AddListField("devices", null, "Onlines");
        //    df.DisplayName = "查看设备";
        //    df.Url = "Device?groupId={Id}";
        //    df.DataVisible = e => (e as CmsFormField).Devices > 0;
        //    df.Target = "_frame";
        //}
        //{
        //    var df = ListFields.GetField("Kind") as ListField;
        //    df.GetValue = e => ((Int32)(e as CmsFormField).Kind).ToString("X4");
        //}
        //ListFields.TraceUrl("TraceId");
    }

    //private readonly ITracer _tracer;

    //public CmsFormFieldController(ITracer tracer)
    //{
    //    _tracer = tracer;
    //}

    protected override ActionResult IndexView(Pager p)
    {
        if (p == null || p["formid"].IsNullOrEmpty())
        {
            var ex = new ErrorModel();
            ex.Exception = new ArgumentException("未指定访问所需要的参数");
            ex.RequestId = DefaultSpan.Current?.TraceId ?? Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ex.Uri = HttpContext.Request.GetRawUrl();
            return View("Error", ex);
        }

        // 需要总记录数来分页
        p.RetrieveTotalCount = true;

        var list = SearchData(p);

        // 用于显示的列
        ViewBag.Fields = OnGetFields(ViewKinds.List, list);
        ViewBag.SearchFields = OnGetFields(ViewKinds.Search, list);

        // Json输出
        if (IsJsonRequest) return Json(0, null, list, new { page = p });

        return View("List", list);
    }

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsFormField> Search(Pager p)
    {
        var formId = p["formid"].ToInt(-1);
        var sorting = p["sorting"].ToInt(-1);
        var enable = p["enable"]?.ToBoolean();
        var fieldType = (LeoChen.Cms.Data.CmsItemType)p["fieldType"].ToInt(-1);

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsFormField.Search(formId, sorting, enable, fieldType, start, end, p["Q"], p);
    }
}