using System.ComponentModel;
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
using static LeoChen.Cms.Data.CmsSite;

namespace LeoChen.Cms.Areas.BasicContent.Controllers;

/// <summary>站点管理表</summary>
[Menu(30, true, Icon = "fa-table")]
[BasicContentArea]
public class CmsSiteController : EntityController<CmsSite>
{
    static CmsSiteController()
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
        //    df.DataVisible = e => (e as CmsSite).Devices > 0;
        //    df.Target = "_frame";
        //}
        //{
        //    var df = ListFields.GetField("Kind") as ListField;
        //    df.GetValue = e => ((Int32)(e as CmsSite).Kind).ToString("X4");
        //}
        //ListFields.TraceUrl("TraceId");
    }

    public override ActionResult Index(Pager p = null)
    {
        var aeraid = CmsAreaContext.CurrentId;

        var entity = FindByAreaID(aeraid);
        if (entity == null)
        {
             entity = new CmsSite();

            // 验证数据权限
            Valid(entity, DataObjectMethodType.Insert, false);

            // 记下添加前的来源页，待会添加成功以后跳转
            // 如果列表页有查询条件，优先使用
            var key = $"Cube_Add_LeoChen.Cms.Data.CmsSite";
            Session[key] = Request.Path.ToString();
            // 用于显示的列
            ViewBag.Fields = OnGetFields(ViewKinds.AddForm, entity);

            return View("AddForm", entity);
        }
        else
        {
            // 验证数据权限
            Valid(entity, DataObjectMethodType.Update, false);
            // 如果列表页有查询条件，优先使用
            var key = $"Cube_Edit_LeoChen.Cms.Data.CmsSite-{entity.ID}";
            Session[key] = Request.Path.ToString();
            // Json输出
            if (IsJsonRequest) return Json(0, null, entity);
            ViewBag.Fields = OnGetFields(ViewKinds.EditForm, entity);
            return View("EditForm", entity);
        }
    }


    protected override FieldCollection OnGetFields(ViewKinds kind, Object model)
    {
        var rs = base.OnGetFields(kind, model);
        rs.RemoveField("AreaID","AreaName");
        return rs;
    }

    //private readonly ITracer _tracer;

    //public CmsSiteController(ITracer tracer)
    //{
    //    _tracer = tracer;
    //}

    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsSite> Search(Pager p)
    {
        var areaId = p["areaId"].ToInt(-1);

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsSite.Search(areaId, start, end, p["Q"], p);
    }
}