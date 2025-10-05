using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>自定义表单字段</summary>
public partial class CmsFormFieldModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 ID { get; set; }

    /// <summary>表单名</summary>
    public Int32 FormID { get; set; }

    /// <summary>字段名</summary>
    public String Name { get; set; }

    /// <summary>显示名</summary>
    public String DisplayName { get; set; }

    /// <summary>字段类型</summary>
    public LeoChen.Cms.Data.CmsItemType FieldType { get; set; }

    /// <summary>长度</summary>
    public Int32 Length { get; set; }

    /// <summary>默认值</summary>
    public String DefaultValue { get; set; }

    /// <summary>描述</summary>
    public String FieldDescription { get; set; }

    /// <summary>排序</summary>
    public Int32 Sorting { get; set; }

    /// <summary>状态</summary>
    public Boolean Enable { get; set; }

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
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ICmsFormField model)
    {
        ID = model.ID;
        FormID = model.FormID;
        Name = model.Name;
        DisplayName = model.DisplayName;
        FieldType = model.FieldType;
        Length = model.Length;
        DefaultValue = model.DefaultValue;
        FieldDescription = model.FieldDescription;
        Sorting = model.Sorting;
        Enable = model.Enable;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUserID = model.UpdateUserID;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
    }
    #endregion
}
