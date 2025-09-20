using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>自定义表单数据</summary>
public partial class CmsExtFormModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 ID { get; set; }

    /// <summary>表单编号</summary>
    public Int32 FormID { get; set; }

    /// <summary>字段编号</summary>
    public Int32 FormFieldID { get; set; }

    /// <summary>默认值</summary>
    public String FieldValue { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ICmsExtForm model)
    {
        ID = model.ID;
        FormID = model.FormID;
        FormFieldID = model.FormFieldID;
        FieldValue = model.FieldValue;
    }
    #endregion
}
