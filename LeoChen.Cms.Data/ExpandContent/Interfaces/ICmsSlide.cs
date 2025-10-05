using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>轮播图片</summary>
public partial interface ICmsSlide
{
    #region 属性
    /// <summary>主键ID</summary>
    Int32 ID { get; set; }

    /// <summary>区域名称</summary>
    Int32 AreaID { get; set; }

    /// <summary>组ID</summary>
    Int32 SlideGroupID { get; set; }

    /// <summary>标题</summary>
    String Title { get; set; }

    /// <summary>副标题</summary>
    String Subtitle { get; set; }

    /// <summary>图片</summary>
    String Pic { get; set; }

    /// <summary>链接</summary>
    String Link { get; set; }

    /// <summary>状态</summary>
    Boolean Enable { get; set; }

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
    #endregion
}
