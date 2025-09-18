using Microsoft.AspNetCore.Mvc;
using LeoChen.Cms.Data;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Web;
using XCode.Membership;
using static LeoChen.Cms.Data.CmsContent_Sort;

namespace LeoChen.Cms.Areas.BasicContent.Controllers;

/// <summary>内容分类表</summary>
[Menu(10, true, Icon = "fa-table")]
[BasicContentArea]
public class CmsContent_SortController : EntityController<CmsContent_Sort>
{
    static CmsContent_SortController()
    {
        //LogOnChange = true;

        //ListFields.RemoveField("Id", "Creator");
        ListFields.RemoveCreateField().RemoveRemarkField();

        //{
        //    var df = ListFields.GetField("Code") as ListField;
        //    df.Url = "?code={Code}";
        //    df.Target = "_blank";
        //}
        //{
        //    var df = ListFields.AddListField("devices", null, "Onlines");
        //    df.DisplayName = "查看设备";
        //    df.Url = "Device?groupId={Id}";
        //    df.DataVisible = e => (e as CmsContent_Sort).Devices > 0;
        //    df.Target = "_frame";
        //}
        //{
        //    var df = ListFields.GetField("Kind") as ListField;
        //    df.GetValue = e => ((Int32)(e as CmsContent_Sort).Kind).ToString("X4");
        //}
        //ListFields.TraceUrl("TraceId");
    }

    //private readonly ITracer _tracer;

    //public CmsContent_SortController(ITracer tracer)
    //{
    //    _tracer = tracer;
    //}

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsContent_Sort> Search(Pager p)
    {
        var areaId = p["areaId"].ToInt(-1);
        var pid = p["pid"].ToInt(-1);
        var modelId = p["modelId"].ToInt(-1);
        var sorting = p["sorting"].ToInt(-1);
        var status = p["status"]?.ToBoolean();

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsContent_Sort.Search(areaId, pid, modelId, sorting, status, start, end, p["Q"], p);
    }
}