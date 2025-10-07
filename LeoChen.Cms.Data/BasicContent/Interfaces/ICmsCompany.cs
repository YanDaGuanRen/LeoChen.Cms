using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LeoChen.Cms.Data;

/// <summary>公司信息表</summary>
public partial interface ICmsCompany
{
    #region 属性
    /// <summary>主键ID</summary>
    Int32 ID { get; set; }

    /// <summary>站点代码</summary>
    Int32 AreaID { get; set; }

    /// <summary>名称。{pboot:companyname}</summary>
    String Name { get; set; }

    /// <summary>地址。{pboot:companyaddress}</summary>
    String Address { get; set; }

    /// <summary>邮编。{pboot:companypostcode}</summary>
    String Postcode { get; set; }

    /// <summary>联系人。{pboot:companycontact}</summary>
    String Contact { get; set; }

    /// <summary>手机号。{pboot:companymobile}</summary>
    String Mobile { get; set; }

    /// <summary>电话。{pboot:companyphone}</summary>
    String Phone { get; set; }

    /// <summary>传真。{pboot:companyfax}</summary>
    String Fax { get; set; }

    /// <summary>邮箱。{pboot:companyemail}</summary>
    String Email { get; set; }

    /// <summary>QQ号。{pboot:companyqq}</summary>
    String QQ { get; set; }

    /// <summary>微信号。{pboot:companyweixin}</summary>
    String Weixin { get; set; }

    /// <summary>营业执照。{pboot:companyblicense}</summary>
    String Blicense { get; set; }

    /// <summary>其他。{pboot:companyother} </summary>
    String Other { get; set; }

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
