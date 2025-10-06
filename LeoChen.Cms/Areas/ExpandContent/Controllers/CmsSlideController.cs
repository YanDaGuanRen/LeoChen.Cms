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
using static LeoChen.Cms.Data.CmsSlide;

namespace LeoChen.Cms.Areas.ExpandContent.Controllers;

/// <summary>轮播图片</summary>
[Menu(60, true, Icon = "fa-table")]
[ExpandContentArea]
public class CmsSlideController : EntityController<CmsSlide>
{
    static CmsSlideController()
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
        //    df.DataVisible = e => (e as CmsSlide).Devices > 0;
        //    df.Target = "_frame";
        //}
        //{
        //    var df = ListFields.GetField("Kind") as ListField;
        //    df.GetValue = e => ((Int32)(e as CmsSlide).Kind).ToString("X4");
        //}
        //ListFields.TraceUrl("TraceId");
    }

    //private readonly ITracer _tracer;

    //public CmsSlideController(ITracer tracer)
    //{
    //    _tracer = tracer;
    //}
    
    protected override FieldCollection OnGetFields(ViewKinds kind, object model)
    {
        var rs = base.OnGetFields(kind, model);
        rs.RemoveField("AreaID","AreaName");
        return rs;
    }

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsSlide> Search(Pager p)
    {
        var areaId = CmsAreaContext.CurrentId;
        var slideGroupId = p["slideGroupId"].ToInt(-1);
        var sorting = p["sorting"].ToInt(-1);
        var enable = p["enable"]?.ToBoolean();

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsSlide.Search(areaId, slideGroupId, sorting, enable, start, end, p["Q"], p);
    }
}