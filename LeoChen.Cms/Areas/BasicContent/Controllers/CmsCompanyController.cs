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
using static LeoChen.Cms.Data.CmsCompany;

namespace LeoChen.Cms.Areas.BasicContent.Controllers;

/// <summary>公司信息表</summary>
[Menu(20, true, Icon = "fa-table")]
[BasicContentArea]
public class CmsCompanyController : EntityController<CmsCompany>
{
    static CmsCompanyController()
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
        //    df.DataVisible = e => (e as CmsCompany).Devices > 0;
        //    df.Target = "_frame";
        //}
        //{
        //    var df = ListFields.GetField("Kind") as ListField;
        //    df.GetValue = e => ((Int32)(e as CmsCompany).Kind).ToString("X4");
        //}
        //ListFields.TraceUrl("TraceId");
    }


    public override ActionResult Index(Pager p = null)
    {
        var aeraid = CmsAreaContext.CurrentId;
        var entity = FindByAreaID(aeraid);
        if (entity == null)
        {
            entity = new CmsCompany();
            Valid(entity, DataObjectMethodType.Insert, false);
            var key = $"Cube_Add_LeoChen.Cms.Data.CmsCompany";
            Session[key] = Request.Path.ToString();
            ViewBag.Fields = OnGetFields(ViewKinds.AddForm, entity);
            return View("AddForm", entity);
        }
        else
        {
            Valid(entity, DataObjectMethodType.Update, false);
            var key = $"Cube_Edit_LeoChen.Cms.Data.CmsCompany-{entity.ID}";
            Session[key] = Request.Path.ToString();
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


    /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
    /// <param name="p">分页器。包含分页排序参数，以及Http请求参数</param>
    /// <returns></returns>
    protected override IEnumerable<CmsCompany> Search(Pager p)
    {
        var areaId = p["areaId"].ToInt(-1);

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return CmsCompany.Search(areaId, start, end, p["Q"], p);
    }
}