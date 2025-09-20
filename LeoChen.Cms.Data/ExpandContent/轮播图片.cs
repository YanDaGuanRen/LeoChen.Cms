using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace LeoChen.Cms.Data;

/// <summary>轮播图片</summary>
[Serializable]
[DataObject]
[Description("轮播图片")]
[BindIndex("IX_CmsSlide_Sorting", false, "Sorting")]
[BindIndex("IX_CmsSlide_SlideGroupID", false, "SlideGroupID")]
[BindTable("CmsSlide", Description = "轮播图片", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class CmsSlide : ICmsSlide, IEntity<ICmsSlide>
{
    #region 属性
    private Int32 _ID;
    /// <summary>主键ID</summary>
    [DisplayName("主键ID")]
    [Description("主键ID")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("ID", "主键ID", "")]
    public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

    private Int32 _SlideGroupID;
    /// <summary>组ID</summary>
    [DisplayName("组ID")]
    [Description("组ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("SlideGroupID", "组ID", "")]
    public Int32 SlideGroupID { get => _SlideGroupID; set { if (OnPropertyChanging("SlideGroupID", value)) { _SlideGroupID = value; OnPropertyChanged("SlideGroupID"); } } }

    private String _Title;
    /// <summary>标题</summary>
    [DisplayName("标题")]
    [Description("标题")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Title", "标题", "", Master = true)]
    public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

    private String _Subtitle;
    /// <summary>副标题</summary>
    [DisplayName("副标题")]
    [Description("副标题")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Subtitle", "副标题", "")]
    public String Subtitle { get => _Subtitle; set { if (OnPropertyChanging("Subtitle", value)) { _Subtitle = value; OnPropertyChanged("Subtitle"); } } }

    private String _Pic;
    /// <summary>图片</summary>
    [DisplayName("图片")]
    [Description("图片")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Pic", "图片", "")]
    public String Pic { get => _Pic; set { if (OnPropertyChanging("Pic", value)) { _Pic = value; OnPropertyChanged("Pic"); } } }

    private String _Link;
    /// <summary>link</summary>
    [DisplayName("link")]
    [Description("link")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Link", "link", "")]
    public String Link { get => _Link; set { if (OnPropertyChanging("Link", value)) { _Link = value; OnPropertyChanged("Link"); } } }

    private Int32 _Sorting;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sorting", "排序", "")]
    public Int32 Sorting { get => _Sorting; set { if (OnPropertyChanging("Sorting", value)) { _Sorting = value; OnPropertyChanged("Sorting"); } } }

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
    public void Copy(ICmsSlide model)
    {
        ID = model.ID;
        SlideGroupID = model.SlideGroupID;
        Title = model.Title;
        Subtitle = model.Subtitle;
        Pic = model.Pic;
        Link = model.Link;
        Sorting = model.Sorting;
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
            "SlideGroupID" => _SlideGroupID,
            "Title" => _Title,
            "Subtitle" => _Subtitle,
            "Pic" => _Pic,
            "Link" => _Link,
            "Sorting" => _Sorting,
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
                case "SlideGroupID": _SlideGroupID = value.ToInt(); break;
                case "Title": _Title = Convert.ToString(value); break;
                case "Subtitle": _Subtitle = Convert.ToString(value); break;
                case "Pic": _Pic = Convert.ToString(value); break;
                case "Link": _Link = Convert.ToString(value); break;
                case "Sorting": _Sorting = value.ToInt(); break;
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
    #endregion

    #region 扩展查询
    /// <summary>根据主键ID查找</summary>
    /// <param name="id">主键ID</param>
    /// <returns>实体对象</returns>
    public static CmsSlide FindByID(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.ID == id);
    }

    /// <summary>根据排序查找</summary>
    /// <param name="sorting">排序</param>
    /// <returns>实体列表</returns>
    public static IList<CmsSlide> FindAllBySorting(Int32 sorting)
    {
        if (sorting < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Sorting == sorting);

        return FindAll(_.Sorting == sorting);
    }

    /// <summary>根据组ID查找</summary>
    /// <param name="slideGroupId">组ID</param>
    /// <returns>实体列表</returns>
    public static IList<CmsSlide> FindAllBySlideGroupID(Int32 slideGroupId)
    {
        if (slideGroupId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.SlideGroupID == slideGroupId);

        return FindAll(_.SlideGroupID == slideGroupId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="slideGroupId">组ID</param>
    /// <param name="sorting">排序</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<CmsSlide> Search(Int32 slideGroupId, Int32 sorting, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (slideGroupId >= 0) exp &= _.SlideGroupID == slideGroupId;
        if (sorting >= 0) exp &= _.Sorting == sorting;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得轮播图片字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>主键ID</summary>
        public static readonly Field ID = FindByName("ID");

        /// <summary>组ID</summary>
        public static readonly Field SlideGroupID = FindByName("SlideGroupID");

        /// <summary>标题</summary>
        public static readonly Field Title = FindByName("Title");

        /// <summary>副标题</summary>
        public static readonly Field Subtitle = FindByName("Subtitle");

        /// <summary>图片</summary>
        public static readonly Field Pic = FindByName("Pic");

        /// <summary>link</summary>
        public static readonly Field Link = FindByName("Link");

        /// <summary>排序</summary>
        public static readonly Field Sorting = FindByName("Sorting");

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

    /// <summary>取得轮播图片字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>主键ID</summary>
        public const String ID = "ID";

        /// <summary>组ID</summary>
        public const String SlideGroupID = "SlideGroupID";

        /// <summary>标题</summary>
        public const String Title = "Title";

        /// <summary>副标题</summary>
        public const String Subtitle = "Subtitle";

        /// <summary>图片</summary>
        public const String Pic = "Pic";

        /// <summary>link</summary>
        public const String Link = "Link";

        /// <summary>排序</summary>
        public const String Sorting = "Sorting";

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
