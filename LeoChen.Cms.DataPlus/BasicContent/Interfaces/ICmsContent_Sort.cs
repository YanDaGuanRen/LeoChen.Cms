using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>内容分类表</summary>
public partial interface ICmsContent_Sort
{
    #region 属性
    /// <summary>主键ID</summary>
    Int32 ID { get; set; }

    /// <summary>租户</summary>
    Int32 TenantId { get; set; }

    /// <summary>区域代码</summary>
    Int32 AreaID { get; set; }

    /// <summary>父级代码</summary>
    Int32 Pid { get; set; }

    /// <summary>模型代码</summary>
    Int32 ModelID { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>副名称</summary>
    String Subname { get; set; }

    /// <summary>列表模板</summary>
    String ListTpl { get; set; }

    /// <summary>内容模板</summary>
    String ContentTpl { get; set; }

    /// <summary>状态</summary>
    Boolean Status { get; set; }

    /// <summary>外部链接</summary>
    String Outlink { get; set; }

    /// <summary>图标</summary>
    String Ico { get; set; }

    /// <summary>图片</summary>
    String Pic { get; set; }

    /// <summary>标题</summary>
    String Title { get; set; }

    /// <summary>关键词</summary>
    String Keywords { get; set; }

    /// <summary>描述</summary>
    String Description { get; set; }

    /// <summary>文件名</summary>
    String Filename { get; set; }

    /// <summary>排序</summary>
    Int32 Sorting { get; set; }

    /// <summary>预留字段1</summary>
    String Def1 { get; set; }

    /// <summary>预留字段2</summary>
    String Def2 { get; set; }

    /// <summary>预留字段3</summary>
    String Def3 { get; set; }

    /// <summary>预留字段1</summary>
    String Def4 { get; set; }

    /// <summary>预留字段2</summary>
    String Def5 { get; set; }

    /// <summary>预留字段3</summary>
    String Def6 { get; set; }

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
