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

/// <summary>站点管理表</summary>
[Serializable]
[DataObject]
[Description("站点管理表")]
[BindIndex("IU_CmsSite_AreaID", true, "AreaID")]
[BindTable("CmsSite", Description = "站点管理表", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class CmsSite : ICmsSite, IEntity<ICmsSite>
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
    /// <summary>区域代码</summary>
    [DisplayName("区域代码")]
    [Description("区域代码")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AreaID", "区域代码", "")]
    public Int32 AreaID { get => _AreaID; set { if (OnPropertyChanging("AreaID", value)) { _AreaID = value; OnPropertyChanged("AreaID"); } } }

    private String _Title;
    /// <summary>标题</summary>
    [DisplayName("标题")]
    [Description("标题")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Title", "标题", "", Master = true)]
    public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

    private String _Subtitle;
    /// <summary>副标题</summary>
    [DisplayName("副标题")]
    [Description("副标题")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Subtitle", "副标题", "")]
    public String Subtitle { get => _Subtitle; set { if (OnPropertyChanging("Subtitle", value)) { _Subtitle = value; OnPropertyChanged("Subtitle"); } } }

    private String _Domain;
    /// <summary>域名</summary>
    [DisplayName("域名")]
    [Description("域名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Domain", "域名", "")]
    public String Domain { get => _Domain; set { if (OnPropertyChanging("Domain", value)) { _Domain = value; OnPropertyChanged("Domain"); } } }

    private String _Logo;
    /// <summary>logo</summary>
    [DisplayName("logo")]
    [Description("logo")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Logo", "logo", "")]
    public String Logo { get => _Logo; set { if (OnPropertyChanging("Logo", value)) { _Logo = value; OnPropertyChanged("Logo"); } } }

    private String _Keywords;
    /// <summary>关键词</summary>
    [DisplayName("关键词")]
    [Description("关键词")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Keywords", "关键词", "")]
    public String Keywords { get => _Keywords; set { if (OnPropertyChanging("Keywords", value)) { _Keywords = value; OnPropertyChanged("Keywords"); } } }

    private String _Description;
    /// <summary>描述</summary>
    [DisplayName("描述")]
    [Description("描述")]
    [DataObjectField(false, false, true, 500)]
    [BindColumn("Description", "描述", "")]
    public String Description { get => _Description; set { if (OnPropertyChanging("Description", value)) { _Description = value; OnPropertyChanged("Description"); } } }

    private String _Icp;
    /// <summary>备案信息</summary>
    [DisplayName("备案信息")]
    [Description("备案信息")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Icp", "备案信息", "")]
    public String Icp { get => _Icp; set { if (OnPropertyChanging("Icp", value)) { _Icp = value; OnPropertyChanged("Icp"); } } }

    private String _Theme;
    /// <summary>模板名</summary>
    [DisplayName("模板名")]
    [Description("模板名")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Theme", "模板名", "")]
    public String Theme { get => _Theme; set { if (OnPropertyChanging("Theme", value)) { _Theme = value; OnPropertyChanged("Theme"); } } }

    private String _Statistical;
    /// <summary>统计代码</summary>
    [DisplayName("统计代码")]
    [Description("统计代码")]
    [DataObjectField(false, false, true, 500)]
    [BindColumn("Statistical", "统计代码", "")]
    public String Statistical { get => _Statistical; set { if (OnPropertyChanging("Statistical", value)) { _Statistical = value; OnPropertyChanged("Statistical"); } } }

    private String _Copyright;
    /// <summary>版权</summary>
    [DisplayName("版权")]
    [Description("版权")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Copyright", "版权", "")]
    public String Copyright { get => _Copyright; set { if (OnPropertyChanging("Copyright", value)) { _Copyright = value; OnPropertyChanged("Copyright"); } } }

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
    public void Copy(ICmsSite model)
    {
        ID = model.ID;
        AreaID = model.AreaID;
        Title = model.Title;
        Subtitle = model.Subtitle;
        Domain = model.Domain;
        Logo = model.Logo;
        Keywords = model.Keywords;
        Description = model.Description;
        Icp = model.Icp;
        Theme = model.Theme;
        Statistical = model.Statistical;
        Copyright = model.Copyright;
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
            "Title" => _Title,
            "Subtitle" => _Subtitle,
            "Domain" => _Domain,
            "Logo" => _Logo,
            "Keywords" => _Keywords,
            "Description" => _Description,
            "Icp" => _Icp,
            "Theme" => _Theme,
            "Statistical" => _Statistical,
            "Copyright" => _Copyright,
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
                case "Title": _Title = Convert.ToString(value); break;
                case "Subtitle": _Subtitle = Convert.ToString(value); break;
                case "Domain": _Domain = Convert.ToString(value); break;
                case "Logo": _Logo = Convert.ToString(value); break;
                case "Keywords": _Keywords = Convert.ToString(value); break;
                case "Description": _Description = Convert.ToString(value); break;
                case "Icp": _Icp = Convert.ToString(value); break;
                case "Theme": _Theme = Convert.ToString(value); break;
                case "Statistical": _Statistical = Convert.ToString(value); break;
                case "Copyright": _Copyright = Convert.ToString(value); break;
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
    /// <summary>区域代码</summary>
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public CmsArea Area => Extends.Get(nameof(Area), k => CmsArea.FindByID(AreaID));

    /// <summary>区域代码</summary>
    [Map(nameof(AreaID), typeof(CmsArea), "ID")]
    public String AreaName => Area?.Name;

    #endregion

    #region 扩展查询
    /// <summary>根据主键ID查找</summary>
    /// <param name="id">主键ID</param>
    /// <returns>实体对象</returns>
    public static CmsSite FindByID(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.ID == id);
    }

    /// <summary>根据区域代码查找</summary>
    /// <param name="areaId">区域代码</param>
    /// <returns>实体对象</returns>
    public static CmsSite FindByAreaID(Int32 areaId)
    {
        if (areaId < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.AreaID == areaId);

        return Find(_.AreaID == areaId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="areaId">区域代码</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<CmsSite> Search(Int32 areaId, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (areaId >= 0) exp &= _.AreaID == areaId;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得站点管理表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>主键ID</summary>
        public static readonly Field ID = FindByName("ID");

        /// <summary>区域代码</summary>
        public static readonly Field AreaID = FindByName("AreaID");

        /// <summary>标题</summary>
        public static readonly Field Title = FindByName("Title");

        /// <summary>副标题</summary>
        public static readonly Field Subtitle = FindByName("Subtitle");

        /// <summary>域名</summary>
        public static readonly Field Domain = FindByName("Domain");

        /// <summary>logo</summary>
        public static readonly Field Logo = FindByName("Logo");

        /// <summary>关键词</summary>
        public static readonly Field Keywords = FindByName("Keywords");

        /// <summary>描述</summary>
        public static readonly Field Description = FindByName("Description");

        /// <summary>备案信息</summary>
        public static readonly Field Icp = FindByName("Icp");

        /// <summary>模板名</summary>
        public static readonly Field Theme = FindByName("Theme");

        /// <summary>统计代码</summary>
        public static readonly Field Statistical = FindByName("Statistical");

        /// <summary>版权</summary>
        public static readonly Field Copyright = FindByName("Copyright");

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

    /// <summary>取得站点管理表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>主键ID</summary>
        public const String ID = "ID";

        /// <summary>区域代码</summary>
        public const String AreaID = "AreaID";

        /// <summary>标题</summary>
        public const String Title = "Title";

        /// <summary>副标题</summary>
        public const String Subtitle = "Subtitle";

        /// <summary>域名</summary>
        public const String Domain = "Domain";

        /// <summary>logo</summary>
        public const String Logo = "Logo";

        /// <summary>关键词</summary>
        public const String Keywords = "Keywords";

        /// <summary>描述</summary>
        public const String Description = "Description";

        /// <summary>备案信息</summary>
        public const String Icp = "Icp";

        /// <summary>模板名</summary>
        public const String Theme = "Theme";

        /// <summary>统计代码</summary>
        public const String Statistical = "Statistical";

        /// <summary>版权</summary>
        public const String Copyright = "Copyright";

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
