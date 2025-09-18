using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>扩展字段表</summary>
public partial interface ICmsExtfield
{
    #region 属性
    /// <summary>主键ID</summary>
    Int32 ID { get; set; }

    /// <summary>租户</summary>
    Int32 TenantId { get; set; }

    /// <summary>模型代码</summary>
    Int32 ContentSortID { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>类型</summary>
    String Type { get; set; }

    /// <summary>值</summary>
    String Value { get; set; }

    /// <summary>描述</summary>
    String Description { get; set; }

    /// <summary>排序</summary>
    Int32 Sorting { get; set; }

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
