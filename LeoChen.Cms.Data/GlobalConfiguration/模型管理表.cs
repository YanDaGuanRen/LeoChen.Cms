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

/// <summary>模型管理表</summary>
[Serializable]
[DataObject]
[Description("模型管理表")]
[BindIndex("IU_CmsModel_Name", true, "Name")]
[BindIndex("IX_CmsModel_ModelType", false, "ModelType")]
[BindTable("CmsModel", Description = "模型管理表", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class CmsModel : ICmsModel, IEntity<ICmsModel>
{
    #region 属性
    private Int32 _ID;
    /// <summary>主键ID</summary>
    [DisplayName("主键ID")]
    [Description("主键ID")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("ID", "主键ID", "")]
    public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

    private String _Name;
    /// <summary>名称</summary>
    [DisplayName("名称")]
    [Description("名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Name", "名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private Boolean _ModelType;
    /// <summary>类型</summary>
    [DisplayName("类型")]
    [Description("类型")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ModelType", "类型", "")]
    public Boolean ModelType { get => _ModelType; set { if (OnPropertyChanging("ModelType", value)) { _ModelType = value; OnPropertyChanged("ModelType"); } } }

    private String _Url;
    /// <summary>Url</summary>
    [DisplayName("Url")]
    [Description("Url")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Url", "Url", "")]
    public String Url { get => _Url; set { if (OnPropertyChanging("Url", value)) { _Url = value; OnPropertyChanged("Url"); } } }

    private String _ListTpl;
    /// <summary>列表模板</summary>
    [DisplayName("列表模板")]
    [Description("列表模板")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("ListTpl", "列表模板", "")]
    public String ListTpl { get => _ListTpl; set { if (OnPropertyChanging("ListTpl", value)) { _ListTpl = value; OnPropertyChanged("ListTpl"); } } }

    private String _ContentTpl;
    /// <summary>内容模板</summary>
    [DisplayName("内容模板")]
    [Description("内容模板")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("ContentTpl", "内容模板", "")]
    public String ContentTpl { get => _ContentTpl; set { if (OnPropertyChanging("ContentTpl", value)) { _ContentTpl = value; OnPropertyChanged("ContentTpl"); } } }

    private Boolean _Status;
    /// <summary>状态</summary>
    [DisplayName("状态")]
    [Description("状态")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Status", "状态", "")]
    public Boolean Status { get => _Status; set { if (OnPropertyChanging("Status", value)) { _Status = value; OnPropertyChanged("Status"); } } }

    private Boolean _IsSystem;
    /// <summary>是否系统模型</summary>
    [DisplayName("是否系统模型")]
    [Description("是否系统模型")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsSystem", "是否系统模型", "")]
    public Boolean IsSystem { get => _IsSystem; set { if (OnPropertyChanging("IsSystem", value)) { _IsSystem = value; OnPropertyChanged("IsSystem"); } } }

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
    public void Copy(ICmsModel model)
    {
        ID = model.ID;
        Name = model.Name;
        ModelType = model.ModelType;
        Url = model.Url;
        ListTpl = model.ListTpl;
        ContentTpl = model.ContentTpl;
        Status = model.Status;
        IsSystem = model.IsSystem;
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
            "Name" => _Name,
            "ModelType" => _ModelType,
            "Url" => _Url,
            "ListTpl" => _ListTpl,
            "ContentTpl" => _ContentTpl,
            "Status" => _Status,
            "IsSystem" => _IsSystem,
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
                case "Name": _Name = Convert.ToString(value); break;
                case "ModelType": _ModelType = value.ToBoolean(); break;
                case "Url": _Url = Convert.ToString(value); break;
                case "ListTpl": _ListTpl = Convert.ToString(value); break;
                case "ContentTpl": _ContentTpl = Convert.ToString(value); break;
                case "Status": _Status = value.ToBoolean(); break;
                case "IsSystem": _IsSystem = value.ToBoolean(); break;
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
    public static CmsModel FindByID(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.ID == id);
    }

    /// <summary>根据名称查找</summary>
    /// <param name="name">名称</param>
    /// <returns>实体对象</returns>
    public static CmsModel FindByName(String name)
    {
        if (name.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Name.EqualIgnoreCase(name));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(name) as CmsModel;

        //return Find(_.Name == name);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="modelType">类型</param>
    /// <param name="status">状态</param>
    /// <param name="isSystem">是否系统模型</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<CmsModel> Search(Boolean? modelType, Boolean? status, Boolean? isSystem, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (modelType != null) exp &= _.ModelType == modelType;
        if (status != null) exp &= _.Status == status;
        if (isSystem != null) exp &= _.IsSystem == isSystem;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得模型管理表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>主键ID</summary>
        public static readonly Field ID = FindByName("ID");

        /// <summary>名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>类型</summary>
        public static readonly Field ModelType = FindByName("ModelType");

        /// <summary>Url</summary>
        public static readonly Field Url = FindByName("Url");

        /// <summary>列表模板</summary>
        public static readonly Field ListTpl = FindByName("ListTpl");

        /// <summary>内容模板</summary>
        public static readonly Field ContentTpl = FindByName("ContentTpl");

        /// <summary>状态</summary>
        public static readonly Field Status = FindByName("Status");

        /// <summary>是否系统模型</summary>
        public static readonly Field IsSystem = FindByName("IsSystem");

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

    /// <summary>取得模型管理表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>主键ID</summary>
        public const String ID = "ID";

        /// <summary>名称</summary>
        public const String Name = "Name";

        /// <summary>类型</summary>
        public const String ModelType = "ModelType";

        /// <summary>Url</summary>
        public const String Url = "Url";

        /// <summary>列表模板</summary>
        public const String ListTpl = "ListTpl";

        /// <summary>内容模板</summary>
        public const String ContentTpl = "ContentTpl";

        /// <summary>状态</summary>
        public const String Status = "Status";

        /// <summary>是否系统模型</summary>
        public const String IsSystem = "IsSystem";

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
