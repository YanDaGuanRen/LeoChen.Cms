using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>模型管理表</summary>
public partial interface ICmsModel
{
    #region 属性
    /// <summary>主键ID</summary>
    Int32 ID { get; set; }

    /// <summary>租户</summary>
    Int32 TenantId { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>类型</summary>
    Boolean ModelType { get; set; }

    /// <summary>Url</summary>
    String Url { get; set; }

    /// <summary>列表模板</summary>
    String ListTpl { get; set; }

    /// <summary>内容模板</summary>
    String ContentTpl { get; set; }

    /// <summary>状态</summary>
    Boolean Status { get; set; }

    /// <summary>是否系统模型</summary>
    Boolean IsSystem { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>更新者</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>备注</summary>
    String Remark { get; set; }
    #endregion
}
