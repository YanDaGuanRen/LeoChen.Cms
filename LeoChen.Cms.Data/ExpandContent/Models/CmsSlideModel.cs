using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>轮播图片</summary>
public partial class CmsSlideModel
{
    #region 属性
    /// <summary>主键ID</summary>
    public Int32 ID { get; set; }

    /// <summary>区域名称</summary>
    public Int32 AreaID { get; set; }

    /// <summary>组ID</summary>
    public Int32 SlideGroupID { get; set; }

    /// <summary>标题</summary>
    public String Title { get; set; }

    /// <summary>副标题</summary>
    public String Subtitle { get; set; }

    /// <summary>图片</summary>
    public String Pic { get; set; }

    /// <summary>链接</summary>
    public String Link { get; set; }

    /// <summary>状态</summary>
    public Boolean Enable { get; set; }

    /// <summary>排序</summary>
    public Int32 Sorting { get; set; }

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
    public void Copy(ICmsSlide model)
    {
        ID = model.ID;
        AreaID = model.AreaID;
        SlideGroupID = model.SlideGroupID;
        Title = model.Title;
        Subtitle = model.Subtitle;
        Pic = model.Pic;
        Link = model.Link;
        Enable = model.Enable;
        Sorting = model.Sorting;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUserID = model.UpdateUserID;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
    }
    #endregion
}
