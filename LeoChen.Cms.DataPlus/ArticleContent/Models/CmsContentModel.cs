using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>内容管理表</summary>
public partial class CmsContentModel
{
    #region 属性
    /// <summary>主键ID</summary>
    public Int32 ID { get; set; }

    /// <summary>租户</summary>
    public Int32 TenantId { get; set; }

    /// <summary>区域代码</summary>
    public Int32 AreaID { get; set; }

    /// <summary>分类代码</summary>
    public Int32 ContentSortID { get; set; }

    /// <summary>子分类代码</summary>
    public Int32 ContentSortSubID { get; set; }

    /// <summary>标题</summary>
    public String Title { get; set; }

    /// <summary>标题颜色</summary>
    public String Titlecolor { get; set; }

    /// <summary>副标题</summary>
    public String Subtitle { get; set; }

    /// <summary>文件名</summary>
    public String Filename { get; set; }

    /// <summary>作者</summary>
    public String Author { get; set; }

    /// <summary>来源</summary>
    public String Source { get; set; }

    /// <summary>外部链接</summary>
    public String Outlink { get; set; }

    /// <summary>日期</summary>
    public DateTime Date { get; set; }

    /// <summary>图标</summary>
    public String Ico { get; set; }

    /// <summary>图片集</summary>
    public String Pics { get; set; }

    /// <summary>内容</summary>
    public String Content { get; set; }

    /// <summary>标签</summary>
    public String Tags { get; set; }

    /// <summary>附件</summary>
    public String Enclosure { get; set; }

    /// <summary>关键词</summary>
    public String Keywords { get; set; }

    /// <summary>描述</summary>
    public String Description { get; set; }

    /// <summary>排序</summary>
    public Int32 Sorting { get; set; }

    /// <summary>状态</summary>
    public Boolean Status { get; set; }

    /// <summary>是否置顶</summary>
    public Boolean IsTop { get; set; }

    /// <summary>是否推荐</summary>
    public Boolean IsRecommend { get; set; }

    /// <summary>是否头条</summary>
    public Boolean IsHeadline { get; set; }

    /// <summary>访问数</summary>
    public Int32 Visits { get; set; }

    /// <summary>点赞数</summary>
    public Int32 Likes { get; set; }

    /// <summary>反对数</summary>
    public Int32 Oppose { get; set; }

    /// <summary>图片标题</summary>
    public String Picstitle { get; set; }

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

    /// <summary>备注</summary>
    public String Remark { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ICmsContent model)
    {
        ID = model.ID;
        TenantId = model.TenantId;
        AreaID = model.AreaID;
        ContentSortID = model.ContentSortID;
        ContentSortSubID = model.ContentSortSubID;
        Title = model.Title;
        Titlecolor = model.Titlecolor;
        Subtitle = model.Subtitle;
        Filename = model.Filename;
        Author = model.Author;
        Source = model.Source;
        Outlink = model.Outlink;
        Date = model.Date;
        Ico = model.Ico;
        Pics = model.Pics;
        Content = model.Content;
        Tags = model.Tags;
        Enclosure = model.Enclosure;
        Keywords = model.Keywords;
        Description = model.Description;
        Sorting = model.Sorting;
        Status = model.Status;
        IsTop = model.IsTop;
        IsRecommend = model.IsRecommend;
        IsHeadline = model.IsHeadline;
        Visits = model.Visits;
        Likes = model.Likes;
        Oppose = model.Oppose;
        Picstitle = model.Picstitle;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUserID = model.UpdateUserID;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
        Remark = model.Remark;
    }
    #endregion
}
