using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>站点管理表</summary>
public partial interface ICmsSite
{
    #region 属性
    /// <summary>主键ID</summary>
    Int32 ID { get; set; }

    /// <summary>区域代码</summary>
    Int32 AreaID { get; set; }

    /// <summary>标题</summary>
    String Title { get; set; }

    /// <summary>副标题</summary>
    String Subtitle { get; set; }

    /// <summary>域名</summary>
    String Domain { get; set; }

    /// <summary>logo</summary>
    String Logo { get; set; }

    /// <summary>关键词</summary>
    String Keywords { get; set; }

    /// <summary>描述</summary>
    String Description { get; set; }

    /// <summary>icp</summary>
    String Icp { get; set; }

    /// <summary>模板名</summary>
    String Theme { get; set; }

    /// <summary>statistical</summary>
    String Statistical { get; set; }

    /// <summary>copyright</summary>
    String Copyright { get; set; }

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
