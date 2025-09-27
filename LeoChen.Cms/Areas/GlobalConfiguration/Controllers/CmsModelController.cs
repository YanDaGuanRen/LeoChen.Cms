using Microsoft.AspNetCore.Mvc;
using LeoChen.Cms.Data;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Web;
using XCode.Membership;
using static LeoChen.Cms.Data.CmsModel;

namespace LeoChen.Cms.Areas.GlobalConfiguration.Controllers;

/// <summary>模型管理表</summary>
[Menu(20, true, Icon = "fa-table")]
[GlobalConfigurationArea]
public class CmsModelController : EntityController<CmsModel>
{
    static CmsModelController()
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
        //    df.DataVisible = e => (e as CmsModel).Devices > 0;
        //    df.Target = "_frame";
        //}
        //{
        //    var df = ListFields.GetField("Kind") as ListField;
        //    df.GetValue = e => ((Int32)(e as CmsModel).Kind).ToString("X4");
        //}
        //ListFields.TraceUrl("TraceId");
    }

    public override ActionResult Add()
    {
        var e = new CmsModel();
        e.Status = true;
        return AddEntity(e);
    }
    //private readonly ITracer _tracer;

    //public CmsModelController(ITracer tracer)
    //{
    //    _tracer = tracer;
    //}

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsModel> Search(Pager p)
    {
        var modelType = p["modelType"]?.ToBoolean();
        var status = p["status"]?.ToBoolean();
        var isSystem = p["isSystem"]?.ToBoolean();

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsModel.Search(modelType, status, isSystem, start, end, p["Q"], p);
    }
}