using LeoChen.Cms.Data;
using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Web;
using XCode.Membership;
using static LeoChen.Cms.Data.CmsModelExtfield;

namespace LeoChen.Cms.Areas.GlobalConfiguration.Controllers;

/// <summary>扩展字段表</summary>
[Menu(10, true, Icon = "fa-table")]
[GlobalConfigurationArea]
public class CmsModelExtfieldController : EntityController<CmsModelExtfield>
{
    static CmsModelExtfieldController()
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
        //    df.DataVisible = e => (e as CmsModelExtfield).Devices > 0;
        //    df.Target = "_frame";
        //}
        //{
        //    var df = ListFields.GetField("Kind") as ListField;
        //    df.GetValue = e => ((Int32)(e as CmsModelExtfield).Kind).ToString("X4");
        //}
        //ListFields.TraceUrl("TraceId");
    }

    //private readonly ITracer _tracer;

    //public CmsModelExtfieldController(ITracer tracer)
    //{
    //    _tracer = tracer;
    //}

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsModelExtfield> Search(Pager p)
    {
        var modelId = p["modelId"].ToInt(-1);
        var type = (LeoChen.Cms.Data.CmsItemType)p["type"].ToInt(-1);
        var enable = p["Enable"]?.ToBoolean();
        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsModelExtfield.Search(modelId, enable,type, start, end, p["Q"], p);
    }
}