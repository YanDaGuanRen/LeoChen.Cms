using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>内容管理表</summary>
public partial interface ICmsContent
{
    #region 属性
    /// <summary>主键ID</summary>
    Int32 ID { get; set; }

    /// <summary>区域代码</summary>
    Int32 AreaID { get; set; }

    /// <summary>分类代码</summary>
    Int32 ContentSortID { get; set; }

    /// <summary>子分类代码</summary>
    Int32 ContentSortSubID { get; set; }

    /// <summary>标题</summary>
    String Title { get; set; }

    /// <summary>标题颜色</summary>
    String Titlecolor { get; set; }

    /// <summary>副标题</summary>
    String Subtitle { get; set; }

    /// <summary>文件名</summary>
    String Filename { get; set; }

    /// <summary>作者</summary>
    String Author { get; set; }

    /// <summary>来源</summary>
    String Source { get; set; }

    /// <summary>外部链接</summary>
    String Outlink { get; set; }

    /// <summary>日期</summary>
    DateTime Date { get; set; }

    /// <summary>图标</summary>
    String Ico { get; set; }

    /// <summary>图片集</summary>
    String Pics { get; set; }

    /// <summary>内容</summary>
    String Content { get; set; }

    /// <summary>标签</summary>
    String Tags { get; set; }

    /// <summary>附件</summary>
    String Enclosure { get; set; }

    /// <summary>关键词</summary>
    String Keywords { get; set; }

    /// <summary>描述</summary>
    String Description { get; set; }

    /// <summary>排序</summary>
    Int32 Sorting { get; set; }

    /// <summary>状态</summary>
    Boolean Status { get; set; }

    /// <summary>是否置顶</summary>
    Boolean IsTop { get; set; }

    /// <summary>是否推荐</summary>
    Boolean IsRecommend { get; set; }

    /// <summary>是否头条</summary>
    Boolean IsHeadline { get; set; }

    /// <summary>访问数</summary>
    Int32 Visits { get; set; }

    /// <summary>点赞数</summary>
    Int32 Likes { get; set; }

    /// <summary>反对数</summary>
    Int32 Oppose { get; set; }

    /// <summary>图片标题</summary>
    String Picstitle { get; set; }

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
