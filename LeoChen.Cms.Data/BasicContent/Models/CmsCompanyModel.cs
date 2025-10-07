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

    /// <summary>站点代码</summary>
    public Int32 AreaID { get; set; }

    /// <summary>名称。{pboot:companyname}</summary>
    public String Name { get; set; }

    /// <summary>地址。{pboot:companyaddress}</summary>
    public String Address { get; set; }

    /// <summary>邮编。{pboot:companypostcode}</summary>
    public String Postcode { get; set; }

    /// <summary>联系人。{pboot:companycontact}</summary>
    public String Contact { get; set; }

    /// <summary>手机号。{pboot:companymobile}</summary>
    public String Mobile { get; set; }

    /// <summary>电话。{pboot:companyphone}</summary>
    public String Phone { get; set; }

    /// <summary>传真。{pboot:companyfax}</summary>
    public String Fax { get; set; }

    /// <summary>邮箱。{pboot:companyemail}</summary>
    public String Email { get; set; }

    /// <summary>QQ号。{pboot:companyqq}</summary>
    public String QQ { get; set; }

    /// <summary>微信号。{pboot:companyweixin}</summary>
    public String Weixin { get; set; }

    /// <summary>营业执照。{pboot:companyblicense}</summary>
    public String Blicense { get; set; }

    /// <summary>其他。{pboot:companyother} </summary>
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
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ICmsCompany model)
    {
        ID = model.ID;
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
    }
    #endregion
}
