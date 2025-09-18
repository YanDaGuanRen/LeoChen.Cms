using Microsoft.AspNetCore.Mvc;
using LeoChen.Cms.Data;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Web;
using XCode.Membership;
using static LeoChen.Cms.Data.CmsContent;

namespace LeoChen.Cms.Areas.ArticleContent.Controllers;

/// <summary>内容管理表</summary>
[Menu(10, true, Icon = "fa-table")]
[ArticleContentArea]
public class CmsContentController : EntityController<CmsContent>
{
    static CmsContentController()
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
        //    df.DataVisible = e => (e as CmsContent).Devices > 0;
        //    df.Target = "_frame";
        //}
        //{
        //    var df = ListFields.GetField("Kind") as ListField;
        //    df.GetValue = e => ((Int32)(e as CmsContent).Kind).ToString("X4");
        //}
        //ListFields.TraceUrl("TraceId");
    }

    //private readonly ITracer _tracer;

    //public CmsContentController(ITracer tracer)
    //{
    //    _tracer = tracer;
    //}

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsContent> Search(Pager p)
    {
        var areaId = p["areaId"].ToInt(-1);
        var contentSortId = p["contentSortId"].ToInt(-1);
        var contentSortSubId = p["contentSortSubId"].ToInt(-1);
        var filename = p["filename"];
        var sorting = p["sorting"].ToInt(-1);
        var status = p["status"]?.ToBoolean();
        var isTop = p["isTop"]?.ToBoolean();
        var isRecommend = p["isRecommend"]?.ToBoolean();
        var isHeadline = p["isHeadline"]?.ToBoolean();

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsContent.Search(areaId, contentSortId, contentSortSubId, filename, sorting, status, isTop, isRecommend, isHeadline, start, end, p["Q"], p);
    }
}