using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using NewLife.Cube.Entity;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Serialization;
using NewLife.Web;
using XCode;
using XCode.Configuration;
using XCode.Membership;

namespace NewLife.Cube;

public partial class EntityController<TEntity, TModel>
{
        
    /// <summary>表单，添加/修改</summary>
    /// <returns></returns>
    [EntityAuthorize(PermissionFlags.Insert)]
    [DisplayName("添加{type}")]
    public virtual ActionResult AddEntity(TEntity entity = null)
    {
        if (entity == null )
        {
            entity = Factory.Create(true) as TEntity;
        }

        // 填充QueryString参数
        var qs = Request.Query;
        foreach (var item in Factory.Fields)
        {
            var v = qs[item.Name];
            if (v.Count > 0) entity[item.Name] = v[0];
        }

        // 验证数据权限
        Valid(entity, DataObjectMethodType.Insert, false);

        // 记下添加前的来源页，待会添加成功以后跳转
        // 如果列表页有查询条件，优先使用
        var key = $"Cube_Add_{typeof(TEntity).FullName}";
        if (Session[CacheKey] is Pager p)
        {
            var sb = p.GetBaseUrl(true, true, true);
            if (sb.Length > 0)
                Session[key] = "Index?" + sb;
            else
                Session[key] = Request.GetReferer();
        }
        else
            Session[key] = Request.GetReferer();

        // 用于显示的列
        ViewBag.Fields = OnGetFields(ViewKinds.AddForm, entity);

        return View("AddForm", entity);
    }
}