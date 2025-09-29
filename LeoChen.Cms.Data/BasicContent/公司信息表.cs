using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Cube.Entity;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace LeoChen.Cms.Data;

/// <summary>公司信息表</summary>
[Serializable]
[DataObject]
[Description("公司信息表")]
[BindIndex("IU_CmsCompany_AreaID", true, "AreaID")]
[BindTable("CmsCompany", Description = "公司信息表", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class CmsCompany : ICmsCompany, IEntity<ICmsCompany>
{
    #region 属性
    private Int32 _ID;
    /// <summary>主键ID</summary>
    [DisplayName("主键ID")]
    [Description("主键ID")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("ID", "主键ID", "")]
    public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

    private Int32 _AreaID;
    /// <summary>站点代码</summary>
    [DisplayName("站点代码")]
    [Description("站点代码")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AreaID", "站点代码", "")]
    public Int32 AreaID { get => _AreaID; set { if (OnPropertyChanging("AreaID", value)) { _AreaID = value; OnPropertyChanged("AreaID"); } } }

    private String _Name;
    /// <summary>名称</summary>
    [DisplayName("名称")]
    [Description("名称")]
    [DataObjectField(false, false, false, 100)]
    [BindColumn("Name", "名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _Address;
    /// <summary>地址</summary>
    [DisplayName("地址")]
    [Description("地址")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Address", "地址", "")]
    public String Address { get => _Address; set { if (OnPropertyChanging("Address", value)) { _Address = value; OnPropertyChanged("Address"); } } }

    private String _Postcode;
    /// <summary>邮编</summary>
    [DisplayName("邮编")]
    [Description("邮编")]
    [DataObjectField(false, false, true, 6)]
    [BindColumn("Postcode", "邮编", "")]
    public String Postcode { get => _Postcode; set { if (OnPropertyChanging("Postcode", value)) { _Postcode = value; OnPropertyChanged("Postcode"); } } }

    private String _Contact;
    /// <summary>联系人</summary>
    [DisplayName("联系人")]
    [Description("联系人")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Contact", "联系人", "")]
    public String Contact { get => _Contact; set { if (OnPropertyChanging("Contact", value)) { _Contact = value; OnPropertyChanged("Contact"); } } }

    private String _Mobile;
    /// <summary>手机号</summary>
    [DisplayName("手机号")]
    [Description("手机号")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Mobile", "手机号", "")]
    public String Mobile { get => _Mobile; set { if (OnPropertyChanging("Mobile", value)) { _Mobile = value; OnPropertyChanged("Mobile"); } } }

    private String _Phone;
    /// <summary>电话</summary>
    [DisplayName("电话")]
    [Description("电话")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Phone", "电话", "")]
    public String Phone { get => _Phone; set { if (OnPropertyChanging("Phone", value)) { _Phone = value; OnPropertyChanged("Phone"); } } }

    private String _Fax;
    /// <summary>传真</summary>
    [DisplayName("传真")]
    [Description("传真")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Fax", "传真", "")]
    public String Fax { get => _Fax; set { if (OnPropertyChanging("Fax", value)) { _Fax = value; OnPropertyChanged("Fax"); } } }

    private String _Email;
    /// <summary>邮箱</summary>
    [DisplayName("邮箱")]
    [Description("邮箱")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Email", "邮箱", "")]
    public String Email { get => _Email; set { if (OnPropertyChanging("Email", value)) { _Email = value; OnPropertyChanged("Email"); } } }

    private String _QQ;
    /// <summary>QQ号</summary>
    [DisplayName("QQ号")]
    [Description("QQ号")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("QQ", "QQ号", "")]
    public String QQ { get => _QQ; set { if (OnPropertyChanging("QQ", value)) { _QQ = value; OnPropertyChanged("QQ"); } } }

    private String _Weixin;
    /// <summary>微信号</summary>
    [DisplayName("微信号")]
    [Description("微信号")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Weixin", "微信号", "")]
    public String Weixin { get => _Weixin; set { if (OnPropertyChanging("Weixin", value)) { _Weixin = value; OnPropertyChanged("Weixin"); } } }

    private String _Blicense;
    /// <summary>营业执照</summary>
    [DisplayName("营业执照")]
    [Description("营业执照")]
    [DataObjectField(false, false, true, 20)]
    [BindColumn("Blicense", "营业执照", "")]
    public String Blicense { get => _Blicense; set { if (OnPropertyChanging("Blicense", value)) { _Blicense = value; OnPropertyChanged("Blicense"); } } }

    private String _Other;
    /// <summary>其他</summary>
    [DisplayName("其他")]
    [Description("其他")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Other", "其他", "")]
    public String Other { get => _Other; set { if (OnPropertyChanging("Other", value)) { _Other = value; OnPropertyChanged("Other"); } } }

    private Int32 _CreateUserID;
    /// <summary>创建者</summary>
    [Category("扩展信息")]
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CreateUserID", "创建者", "")]
    public Int32 CreateUserID { get => _CreateUserID; set { if (OnPropertyChanging("CreateUserID", value)) { _CreateUserID = value; OnPropertyChanged("CreateUserID"); } } }

    private DateTime _CreateTime;
    /// <summary>创建时间</summary>
    [Category("扩展信息")]
    [DisplayName("创建时间")]
    [Description("创建时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "创建时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

    private String _CreateIP;
    /// <summary>创建地址</summary>
    [Category("扩展信息")]
    [DisplayName("创建地址")]
    [Description("创建地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateIP", "创建地址", "")]
    public String CreateIP { get => _CreateIP; set { if (OnPropertyChanging("CreateIP", value)) { _CreateIP = value; OnPropertyChanged("CreateIP"); } } }

    private Int32 _UpdateUserID;
    /// <summary>更新者</summary>
    [Category("扩展信息")]
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UpdateUserID", "更新者", "")]
    public Int32 UpdateUserID { get => _UpdateUserID; set { if (OnPropertyChanging("UpdateUserID", value)) { _UpdateUserID = value; OnPropertyChanged("UpdateUserID"); } } }

    private DateTime _UpdateTime;
    /// <summary>更新时间</summary>
    [Category("扩展信息")]
    [DisplayName("更新时间")]
    [Description("更新时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("UpdateTime", "更新时间", "")]
    public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }

    private String _UpdateIP;
    /// <summary>更新地址</summary>
    [Category("扩展信息")]
    [DisplayName("更新地址")]
    [Description("更新地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UpdateIP", "更新地址", "")]
    public String UpdateIP { get => _UpdateIP; set { if (OnPropertyChanging("UpdateIP", value)) { _UpdateIP = value; OnPropertyChanged("UpdateIP"); } } }
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object this[String name]
    {
        get => name switch
        {
            "ID" => _ID,
            "AreaID" => _AreaID,
            "Name" => _Name,
            "Address" => _Address,
            "Postcode" => _Postcode,
            "Contact" => _Contact,
            "Mobile" => _Mobile,
            "Phone" => _Phone,
            "Fax" => _Fax,
            "Email" => _Email,
            "QQ" => _QQ,
            "Weixin" => _Weixin,
            "Blicense" => _Blicense,
            "Other" => _Other,
            "CreateUserID" => _CreateUserID,
            "CreateTime" => _CreateTime,
            "CreateIP" => _CreateIP,
            "UpdateUserID" => _UpdateUserID,
            "UpdateTime" => _UpdateTime,
            "UpdateIP" => _UpdateIP,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "ID": _ID = value.ToInt(); break;
                case "AreaID": _AreaID = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Address": _Address = Convert.ToString(value); break;
                case "Postcode": _Postcode = Convert.ToString(value); break;
                case "Contact": _Contact = Convert.ToString(value); break;
                case "Mobile": _Mobile = Convert.ToString(value); break;
                case "Phone": _Phone = Convert.ToString(value); break;
                case "Fax": _Fax = Convert.ToString(value); break;
                case "Email": _Email = Convert.ToString(value); break;
                case "QQ": _QQ = Convert.ToString(value); break;
                case "Weixin": _Weixin = Convert.ToString(value); break;
                case "Blicense": _Blicense = Convert.ToString(value); break;
                case "Other": _Other = Convert.ToString(value); break;
                case "CreateUserID": _CreateUserID = value.ToInt(); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "CreateIP": _CreateIP = Convert.ToString(value); break;
                case "UpdateUserID": _UpdateUserID = value.ToInt(); break;
                case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                case "UpdateIP": _UpdateIP = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    /// <summary>站点代码</summary>
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public CmsArea Area => Extends.Get(nameof(Area), k => CmsArea.FindByID(AreaID));

    /// <summary>站点代码</summary>
    [Map(nameof(AreaID), typeof(CmsArea), "ID")]
    public String AreaName => Area?.Name;

    #endregion

    #region 扩展查询
    /// <summary>根据主键ID查找</summary>
    /// <param name="id">主键ID</param>
    /// <returns>实体对象</returns>
    public static CmsCompany FindByID(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.ID == id);
    }

    /// <summary>根据站点代码查找</summary>
    /// <param name="areaId">站点代码</param>
    /// <returns>实体对象</returns>
    public static CmsCompany FindByAreaID(Int32 areaId)
    {
        if (areaId < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.AreaID == areaId);

        return Find(_.AreaID == areaId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="areaId">站点代码</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<CmsCompany> Search(Int32 areaId, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (areaId >= 0) exp &= _.AreaID == areaId;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得公司信息表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>主键ID</summary>
        public static readonly Field ID = FindByName("ID");

        /// <summary>站点代码</summary>
        public static readonly Field AreaID = FindByName("AreaID");

        /// <summary>名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>地址</summary>
        public static readonly Field Address = FindByName("Address");

        /// <summary>邮编</summary>
        public static readonly Field Postcode = FindByName("Postcode");

        /// <summary>联系人</summary>
        public static readonly Field Contact = FindByName("Contact");

        /// <summary>手机号</summary>
        public static readonly Field Mobile = FindByName("Mobile");

        /// <summary>电话</summary>
        public static readonly Field Phone = FindByName("Phone");

        /// <summary>传真</summary>
        public static readonly Field Fax = FindByName("Fax");

        /// <summary>邮箱</summary>
        public static readonly Field Email = FindByName("Email");

        /// <summary>QQ号</summary>
        public static readonly Field QQ = FindByName("QQ");

        /// <summary>微信号</summary>
        public static readonly Field Weixin = FindByName("Weixin");

        /// <summary>营业执照</summary>
        public static readonly Field Blicense = FindByName("Blicense");

        /// <summary>其他</summary>
        public static readonly Field Other = FindByName("Other");

        /// <summary>创建者</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>创建地址</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUserID = FindByName("UpdateUserID");

        /// <summary>更新时间</summary>
        public static readonly Field UpdateTime = FindByName("UpdateTime");

        /// <summary>更新地址</summary>
        public static readonly Field UpdateIP = FindByName("UpdateIP");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得公司信息表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>主键ID</summary>
        public const String ID = "ID";

        /// <summary>站点代码</summary>
        public const String AreaID = "AreaID";

        /// <summary>名称</summary>
        public const String Name = "Name";

        /// <summary>地址</summary>
        public const String Address = "Address";

        /// <summary>邮编</summary>
        public const String Postcode = "Postcode";

        /// <summary>联系人</summary>
        public const String Contact = "Contact";

        /// <summary>手机号</summary>
        public const String Mobile = "Mobile";

        /// <summary>电话</summary>
        public const String Phone = "Phone";

        /// <summary>传真</summary>
        public const String Fax = "Fax";

        /// <summary>邮箱</summary>
        public const String Email = "Email";

        /// <summary>QQ号</summary>
        public const String QQ = "QQ";

        /// <summary>微信号</summary>
        public const String Weixin = "Weixin";

        /// <summary>营业执照</summary>
        public const String Blicense = "Blicense";

        /// <summary>其他</summary>
        public const String Other = "Other";

        /// <summary>创建者</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>创建地址</summary>
        public const String CreateIP = "CreateIP";

        /// <summary>更新者</summary>
        public const String UpdateUserID = "UpdateUserID";

        /// <summary>更新时间</summary>
        public const String UpdateTime = "UpdateTime";

        /// <summary>更新地址</summary>
        public const String UpdateIP = "UpdateIP";
    }
    #endregion
}
