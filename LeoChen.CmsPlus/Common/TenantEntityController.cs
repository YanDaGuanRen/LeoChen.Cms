using System.ComponentModel;
using NewLife.Cube.ViewModels;
using XCode;
using XCode.Membership;

namespace LenChen.Cms.Common;

public class TenantEntityController<TEntity> : TenantEntityController<TEntity,TEntity>
    where TEntity : Entity<TEntity>,ITenantSource, new()
{
    
}

public class TenantEntityController<TEntity,TModel> : EntityController<TEntity,TModel> 
    where TEntity : Entity<TEntity>,ITenantSource, new()
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