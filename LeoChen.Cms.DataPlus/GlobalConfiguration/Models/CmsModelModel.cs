using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>模型管理表</summary>
public partial class CmsModelModel
{
    #region 属性
    /// <summary>主键ID</summary>
    public Int32 ID { get; set; }

    /// <summary>租户</summary>
    public Int32 TenantId { get; set; }

    /// <summary>名称</summary>
    public String Name { get; set; }

    /// <summary>类型</summary>
    public Boolean ModelType { get; set; }

    /// <summary>Url</summary>
    public String Url { get; set; }

    /// <summary>列表模板</summary>
    public String ListTpl { get; set; }

    /// <summary>内容模板</summary>
    public String ContentTpl { get; set; }

    /// <summary>状态</summary>
    public Boolean Status { get; set; }

    /// <summary>是否系统模型</summary>
    public Boolean IsSystem { get; set; }

    /// <summary>创建者</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>更新者</summary>
    public Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    public String UpdateIP { get; set; }

    /// <summary>备注</summary>
    public String Remark { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ICmsModel model)
    {
        ID = model.ID;
        TenantId = model.TenantId;
        Name = model.Name;
        ModelType = model.ModelType;
        Url = model.Url;
        ListTpl = model.ListTpl;
        ContentTpl = model.ContentTpl;
        Status = model.Status;
        IsSystem = model.IsSystem;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUserID = model.UpdateUserID;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
        Remark = model.Remark;
    }
    #endregion
}
