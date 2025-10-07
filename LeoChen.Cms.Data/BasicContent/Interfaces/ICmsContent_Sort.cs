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
    /// <summary>主键ID。[nav:scode]</summary>
    Int32 ID { get; set; }

    /// <summary>区域代码</summary>
    Int32 AreaID { get; set; }

    /// <summary>父级代码。[nav:pcode]</summary>
    Int32 Pid { get; set; }

    /// <summary>模型代码</summary>
    Int32 ModelID { get; set; }

    /// <summary>名称。[nav:name]</summary>
    String Name { get; set; }

    /// <summary>副名称。[nav:subname]</summary>
    String Subname { get; set; }

    /// <summary>URL名称。[nav:link]</summary>
    String UrlName { get; set; }

    /// <summary>列表模板。[nav:listtpl]</summary>
    String ListTpl { get; set; }

    /// <summary>内容模板。[nav:contenttpl]</summary>
    String ContentTpl { get; set; }

    /// <summary>状态。</summary>
    Boolean Enable { get; set; }

    /// <summary>外部链接。[nav:outlink]</summary>
    String Outlink { get; set; }

    /// <summary>图标。[nav:ico]</summary>
    String Ico { get; set; }

    /// <summary>图片。[nav:pic]</summary>
    String Pic { get; set; }

    /// <summary>标题。[nav:title]</summary>
    String Title { get; set; }

    /// <summary>关键词。[nav:keywords]</summary>
    String Keywords { get; set; }

    /// <summary>描述。[nav:description]</summary>
    String Description { get; set; }

    /// <summary>文件名。[nav:filename]</summary>
    String Filename { get; set; }

    /// <summary>排序。</summary>
    Int32 Sorting { get; set; }

    /// <summary>预留字段1。[nav:def1]</summary>
    String Def1 { get; set; }

    /// <summary>预留字段2。[nav:def2]</summary>
    String Def2 { get; set; }

    /// <summary>预留字段3。[nav:def3]</summary>
    String Def3 { get; set; }

    /// <summary>预留字段4。[nav:def4]</summary>
    String Def4 { get; set; }

    /// <summary>预留字段5。[nav:def5]</summary>
    String Def5 { get; set; }

    /// <summary>预留字段6。[nav:def6]</summary>
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
    #endregion
}
