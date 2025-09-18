using System.ComponentModel;
using NewLife.Cube.Entity;
using NewLife.Cube.ViewModels;
using NewLife.Log;
using NewLife.Serialization;
using XCode;
using XCode.Configuration;
using XCode.Membership;

namespace NewLife.Cube;

/// <summary>实体控制器基类</summary>
/// <typeparam name="TEntity"></typeparam>
public partial class EntityController<TEntity> : EntityController<TEntity, TEntity> where TEntity : Entity<TEntity>, ITenantSource, new() { }

/// <summary>实体控制器基类</summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TModel"></typeparam>
public partial class EntityController<TEntity, TModel> : ReadOnlyEntityController<TEntity> where TEntity : Entity<TEntity>,  ITenantSource,new()
{

    /// <summary>获取字段信息</summary>
    /// <param name="kind"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    protected override FieldCollection OnGetFields(ViewKinds kind, Object model)
    {
        var rs = base.OnGetFields(kind, model);
        rs.RemoveField("TenantId", "TenantName");
        if (TenantContext.CurrentId > 0)
        {
            rs.RemoveField("TenantId", "TenantName");
            // switch (kind)
            // {
            //     case ViewKinds.Detail:
            //         rs.RemoveField("TenantId", "TenantName");
            //         break;
            //     case ViewKinds.AddForm:
            //         rs.RemoveField("TenantId", "TenantName");
            //         break;
            //     case ViewKinds.EditForm:
            //         rs.RemoveField("TenantId", "TenantName");
            //         break;
            //     default:
            //         break;
            // }
        }
        return rs;
    }

    protected override bool Valid(TEntity entity, DataObjectMethodType type, bool post)
    {
        // if (type == DataObjectMethodType.Insert)
        // {
        //     if (entity.TenantId == 0) entity.TenantId = TenantContext.CurrentId;
        // }

        if (entity.TenantId < 1) entity.TenantId = TenantContext.CurrentId;
        return base.Valid(entity, type, post);
    }

}