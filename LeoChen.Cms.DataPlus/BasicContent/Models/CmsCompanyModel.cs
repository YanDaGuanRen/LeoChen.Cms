using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>公司信息表</summary>
public partial class CmsCompanyModel
{
    #region 属性
    /// <summary>主键ID</summary>
    public Int32 ID { get; set; }

    /// <summary>租户</summary>
    public Int32 TenantId { get; set; }

    /// <summary>站点代码</summary>
    public Int32 AreaID { get; set; }

    /// <summary>名称</summary>
    public String Name { get; set; }

    /// <summary>地址</summary>
    public String Address { get; set; }

    /// <summary>邮编</summary>
    public String Postcode { get; set; }

    /// <summary>联系人</summary>
    public String Contact { get; set; }

    /// <summary>手机号</summary>
    public String Mobile { get; set; }

    /// <summary>电话</summary>
    public String Phone { get; set; }

    /// <summary>传真</summary>
    public String Fax { get; set; }

    /// <summary>邮箱</summary>
    public String Email { get; set; }

    /// <summary>QQ号</summary>
    public String QQ { get; set; }

    /// <summary>微信号</summary>
    public String Weixin { get; set; }

    /// <summary>营业执照</summary>
    public String Blicense { get; set; }

    /// <summary>其他</summary>
    public String Other { get; set; }

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
    public void Copy(ICmsCompany model)
    {
        ID = model.ID;
        TenantId = model.TenantId;
        AreaID = model.AreaID;
        Name = model.Name;
        Address = model.Address;
        Postcode = model.Postcode;
        Contact = model.Contact;
        Mobile = model.Mobile;
        Phone = model.Phone;
        Fax = model.Fax;
        Email = model.Email;
        QQ = model.QQ;
        Weixin = model.Weixin;
        Blicense = model.Blicense;
        Other = model.Other;
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
