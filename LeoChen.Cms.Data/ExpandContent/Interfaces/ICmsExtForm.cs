using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>自定义表单数据</summary>
public partial interface ICmsExtForm
{
    #region 属性
    /// <summary>编号</summary>
    Int32 ID { get; set; }

    /// <summary>表单编号</summary>
    Int32 FormID { get; set; }

    /// <summary>字段编号</summary>
    Int32 FormFieldID { get; set; }

    /// <summary>默认值</summary>
    String FieldValue { get; set; }
    #endregion
}
