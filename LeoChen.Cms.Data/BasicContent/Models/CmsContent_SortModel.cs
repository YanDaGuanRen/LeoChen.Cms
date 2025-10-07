using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>内容分类表</summary>
public partial class CmsContent_SortModel
{
    #region 属性
    /// <summary>主键ID。[nav:scode]</summary>
    public Int32 ID { get; set; }

    /// <summary>区域代码</summary>
    public Int32 AreaID { get; set; }

    /// <summary>父级代码。[nav:pcode]</summary>
    public Int32 Pid { get; set; }

    /// <summary>模型代码</summary>
    public Int32 ModelID { get; set; }

    /// <summary>名称。[nav:name]</summary>
    public String Name { get; set; }

    /// <summary>副名称。[nav:subname]</summary>
    public String Subname { get; set; }

    /// <summary>URL名称。[nav:link]</summary>
    public String UrlName { get; set; }

    /// <summary>列表模板。[nav:listtpl]</summary>
    public String ListTpl { get; set; }

    /// <summary>内容模板。[nav:contenttpl]</summary>
    public String ContentTpl { get; set; }

    /// <summary>状态。</summary>
    public Boolean Enable { get; set; }

    /// <summary>外部链接。[nav:outlink]</summary>
    public String Outlink { get; set; }

    /// <summary>图标。[nav:ico]</summary>
    public String Ico { get; set; }

    /// <summary>图片。[nav:pic]</summary>
    public String Pic { get; set; }

    /// <summary>标题。[nav:title]</summary>
    public String Title { get; set; }

    /// <summary>关键词。[nav:keywords]</summary>
    public String Keywords { get; set; }

    /// <summary>描述。[nav:description]</summary>
    public String Description { get; set; }

    /// <summary>文件名。[nav:filename]</summary>
    public String Filename { get; set; }

    /// <summary>排序。</summary>
    public Int32 Sorting { get; set; }

    /// <summary>预留字段1。[nav:def1]</summary>
    public String Def1 { get; set; }

    /// <summary>预留字段2。[nav:def2]</summary>
    public String Def2 { get; set; }

    /// <summary>预留字段3。[nav:def3]</summary>
    public String Def3 { get; set; }

    /// <summary>预留字段4。[nav:def4]</summary>
    public String Def4 { get; set; }

    /// <summary>预留字段5。[nav:def5]</summary>
    public String Def5 { get; set; }

    /// <summary>预留字段6。[nav:def6]</summary>
    public String Def6 { get; set; }

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
    public void Copy(ICmsContent_Sort model)
    {
        ID = model.ID;
        AreaID = model.AreaID;
        Pid = model.Pid;
        ModelID = model.ModelID;
        Name = model.Name;
        Subname = model.Subname;
        UrlName = model.UrlName;
        ListTpl = model.ListTpl;
        ContentTpl = model.ContentTpl;
        Enable = model.Enable;
        Outlink = model.Outlink;
        Ico = model.Ico;
        Pic = model.Pic;
        Title = model.Title;
        Keywords = model.Keywords;
        Description = model.Description;
        Filename = model.Filename;
        Sorting = model.Sorting;
        Def1 = model.Def1;
        Def2 = model.Def2;
        Def3 = model.Def3;
        Def4 = model.Def4;
        Def5 = model.Def5;
        Def6 = model.Def6;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUserID = model.UpdateUserID;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
    }
    #endregion
}
