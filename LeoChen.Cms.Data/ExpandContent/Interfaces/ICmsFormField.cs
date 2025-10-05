using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>自定义表单字段</summary>
public partial interface ICmsFormField
{
    #region 属性
    /// <summary>编号</summary>
    Int32 ID { get; set; }

    /// <summary>表单名</summary>
    Int32 FormID { get; set; }

    /// <summary>字段名</summary>
    String Name { get; set; }

    /// <summary>显示名</summary>
    String DisplayName { get; set; }

    /// <summary>字段类型</summary>
    LeoChen.Cms.Data.CmsItemType FieldType { get; set; }

    /// <summary>长度</summary>
    Int32 Length { get; set; }

    /// <summary>默认值</summary>
    String DefaultValue { get; set; }

    /// <summary>描述</summary>
    String FieldDescription { get; set; }

    /// <summary>排序</summary>
    Int32 Sorting { get; set; }

    /// <summary>状态</summary>
    Boolean Enable { get; set; }

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
    #endregion
}
