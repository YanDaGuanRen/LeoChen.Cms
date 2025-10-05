using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>友情链接</summary>
public partial class CmsLinkModel
{
    #region 属性
    /// <summary>主键ID</summary>
    public Int32 ID { get; set; }

    /// <summary>区域名称</summary>
    public Int32 AreaID { get; set; }

    /// <summary>组ID</summary>
    public Int32 LinkGroupID { get; set; }

    /// <summary>名称</summary>
    public String Name { get; set; }

    /// <summary>友情链接</summary>
    public String Link { get; set; }

    /// <summary>图标</summary>
    public String Logo { get; set; }

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
    public void Copy(ICmsLink model)
    {
        ID = model.ID;
        AreaID = model.AreaID;
        LinkGroupID = model.LinkGroupID;
        Name = model.Name;
        Link = model.Link;
        Logo = model.Logo;
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
