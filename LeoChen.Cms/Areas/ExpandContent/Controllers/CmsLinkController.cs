using Microsoft.AspNetCore.Mvc;
using LeoChen.Cms.Data;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Common;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Web;
using XCode.Membership;
using static LeoChen.Cms.Data.CmsLink;

namespace LeoChen.Cms.Areas.ExpandContent.Controllers;

/// <summary>友情链接</summary>
[Menu(50, true, Icon = "fa-table")]
[ExpandContentArea]
public class CmsLinkController : EntityController<CmsLink>
{
    static CmsLinkController()
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
        //    df.DataVisible = e => (e as CmsLink).Devices > 0;
        //    df.Target = "_frame";
        //}
        //{
        //    var df = ListFields.GetField("Kind") as ListField;
        //    df.GetValue = e => ((Int32)(e as CmsLink).Kind).ToString("X4");
        //}
        //ListFields.TraceUrl("TraceId");
    }

    //private readonly ITracer _tracer;

    //public CmsLinkController(ITracer tracer)
    //{
    //    _tracer = tracer;
    //}

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsLink> Search(Pager p)
    {
        var areaId = CmsAreaContext.CurrentId;
        var linkGroupId = p["linkGroupId"].ToInt(-1);
        var sorting = p["sorting"].ToInt(-1);
        var enable = p["enable"]?.ToBoolean();

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsLink.Search(areaId, linkGroupId, sorting, enable, start, end, p["Q"], p);
    }
}