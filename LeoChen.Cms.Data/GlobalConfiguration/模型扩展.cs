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

/// <summary>模型扩展</summary>
[Serializable]
[DataObject]
[Description("模型扩展")]
[BindIndex("IU_CmsModelExtfield_Name", true, "Name")]
[BindIndex("IX_CmsModelExtfield_ModelID", false, "ModelID")]
[BindTable("CmsModelExtfield", Description = "模型扩展", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class CmsModelExtfield : ICmsModelExtfield, IEntity<ICmsModelExtfield>
{
    #region 属性
    private Int32 _ID;
    /// <summary>主键ID</summary>
    [DisplayName("主键ID")]
    [Description("主键ID")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("ID", "主键ID", "")]
    public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

    private Int32 _ModelID;
    /// <summary>模型代码</summary>
    [DisplayName("模型代码")]
    [Description("模型代码")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ModelID", "模型代码", "")]
    public Int32 ModelID { get => _ModelID; set { if (OnPropertyChanging("ModelID", value)) { _ModelID = value; OnPropertyChanged("ModelID"); } } }

    private String _Name;
    /// <summary>名称</summary>
    [DisplayName("名称")]
    [Description("名称")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Name", "名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _DisplayName;
    /// <summary>显示名称</summary>
    [DisplayName("显示名称")]
    [Description("显示名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("DisplayName", "显示名称", "")]
    public String DisplayName { get => _DisplayName; set { if (OnPropertyChanging("DisplayName", value)) { _DisplayName = value; OnPropertyChanged("DisplayName"); } } }

    private LeoChen.Cms.Data.CmsItemType _FieldType;
    /// <summary>类型</summary>
    [DisplayName("类型")]
    [Description("类型")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("FieldType", "类型", "")]
    public LeoChen.Cms.Data.CmsItemType FieldType { get => _FieldType; set { if (OnPropertyChanging("FieldType", value)) { _FieldType = value; OnPropertyChanged("FieldType"); } } }

    private String _Value;
    /// <summary>值</summary>
    [DisplayName("值")]
    [Description("值")]
    [DataObjectField(false, false, true, 2000)]
    [BindColumn("Value", "值", "")]
    public String Value { get => _Value; set { if (OnPropertyChanging("Value", value)) { _Value = value; OnPropertyChanged("Value"); } } }

    private String _Description;
    /// <summary>描述</summary>
    [DisplayName("描述")]
    [Description("描述")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Description", "描述", "")]
    public String Description { get => _Description; set { if (OnPropertyChanging("Description", value)) { _Description = value; OnPropertyChanged("Description"); } } }

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
    public void Copy(ICmsModelExtfield model)
    {
        ID = model.ID;
        ModelID = model.ModelID;
        Name = model.Name;
        DisplayName = model.DisplayName;
        FieldType = model.FieldType;
        Value = model.Value;
        Description = model.Description;
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
            "ModelID" => _ModelID,
            "Name" => _Name,
            "DisplayName" => _DisplayName,
            "FieldType" => _FieldType,
            "Value" => _Value,
            "Description" => _Description,
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
                case "ModelID": _ModelID = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "DisplayName": _DisplayName = Convert.ToString(value); break;
                case "FieldType": _FieldType = (LeoChen.Cms.Data.CmsItemType)value.ToInt(); break;
                case "Value": _Value = Convert.ToString(value); break;
                case "Description": _Description = Convert.ToString(value); break;
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
    /// <summary>模型代码</summary>
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public CmsModel Model => Extends.Get(nameof(Model), k => CmsModel.FindByID(ModelID));

    /// <summary>模型代码</summary>
    [Map(nameof(ModelID), typeof(CmsModel), "ID")]
    public String ModelName => Model?.Name;

    #endregion

    #region 扩展查询
    /// <summary>根据主键ID查找</summary>
    /// <param name="id">主键ID</param>
    /// <returns>实体对象</returns>
    public static CmsModelExtfield FindByID(Int32 id)
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
    public static CmsModelExtfield FindByName(String name)
    {
        if (name.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Name.EqualIgnoreCase(name));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(name) as CmsModelExtfield;

        //return Find(_.Name == name);
    }

    /// <summary>根据模型代码查找</summary>
    /// <param name="modelId">模型代码</param>
    /// <returns>实体列表</returns>
    public static IList<CmsModelExtfield> FindAllByModelID(Int32 modelId)
    {
        if (modelId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ModelID == modelId);

        return FindAll(_.ModelID == modelId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="modelId">模型代码</param>
    /// <param name="fieldType">类型</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<CmsModelExtfield> Search(Int32 modelId, LeoChen.Cms.Data.CmsItemType fieldType, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (modelId >= 0) exp &= _.ModelID == modelId;
        if (fieldType >= 0) exp &= _.FieldType == fieldType;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得模型扩展字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>主键ID</summary>
        public static readonly Field ID = FindByName("ID");

        /// <summary>模型代码</summary>
        public static readonly Field ModelID = FindByName("ModelID");

        /// <summary>名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>显示名称</summary>
        public static readonly Field DisplayName = FindByName("DisplayName");

        /// <summary>类型</summary>
        public static readonly Field FieldType = FindByName("FieldType");

        /// <summary>值</summary>
        public static readonly Field Value = FindByName("Value");

        /// <summary>描述</summary>
        public static readonly Field Description = FindByName("Description");

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

    /// <summary>取得模型扩展字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>主键ID</summary>
        public const String ID = "ID";

        /// <summary>模型代码</summary>
        public const String ModelID = "ModelID";

        /// <summary>名称</summary>
        public const String Name = "Name";

        /// <summary>显示名称</summary>
        public const String DisplayName = "DisplayName";

        /// <summary>类型</summary>
        public const String FieldType = "FieldType";

        /// <summary>值</summary>
        public const String Value = "Value";

        /// <summary>描述</summary>
        public const String Description = "Description";

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
