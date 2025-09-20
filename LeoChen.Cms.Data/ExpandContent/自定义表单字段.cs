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

/// <summary>自定义表单字段</summary>
[Serializable]
[DataObject]
[Description("自定义表单字段")]
[BindIndex("IU_CmsFormField_Name", true, "Name")]
[BindIndex("IX_CmsFormField_FormID", false, "FormID")]
[BindIndex("IX_CmsFormField_Enable", false, "Enable")]
[BindTable("CmsFormField", Description = "自定义表单字段", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class CmsFormField : ICmsFormField, IEntity<ICmsFormField>
{
    #region 属性
    private Int32 _ID;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("ID", "编号", "")]
    public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

    private Int32 _FormID;
    /// <summary>表单编号</summary>
    [DisplayName("表单编号")]
    [Description("表单编号")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("FormID", "表单编号", "")]
    public Int32 FormID { get => _FormID; set { if (OnPropertyChanging("FormID", value)) { _FormID = value; OnPropertyChanged("FormID"); } } }

    private String _Name;
    /// <summary>字段名</summary>
    [DisplayName("字段名")]
    [Description("字段名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Name", "字段名", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private LeoChen.Cms.Data.CmsItemType _FieldType;
    /// <summary>字段类型</summary>
    [DisplayName("字段类型")]
    [Description("字段类型")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("FieldType", "字段类型", "")]
    public LeoChen.Cms.Data.CmsItemType FieldType { get => _FieldType; set { if (OnPropertyChanging("FieldType", value)) { _FieldType = value; OnPropertyChanged("FieldType"); } } }

    private String _DisplayName;
    /// <summary>显示名</summary>
    [DisplayName("显示名")]
    [Description("显示名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("DisplayName", "显示名", "")]
    public String DisplayName { get => _DisplayName; set { if (OnPropertyChanging("DisplayName", value)) { _DisplayName = value; OnPropertyChanged("DisplayName"); } } }

    private Int32 _Length;
    /// <summary>长度</summary>
    [DisplayName("长度")]
    [Description("长度")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Length", "长度", "")]
    public Int32 Length { get => _Length; set { if (OnPropertyChanging("Length", value)) { _Length = value; OnPropertyChanged("Length"); } } }

    private String _DefaultValue;
    /// <summary>默认值</summary>
    [DisplayName("默认值")]
    [Description("默认值")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("DefaultValue", "默认值", "")]
    public String DefaultValue { get => _DefaultValue; set { if (OnPropertyChanging("DefaultValue", value)) { _DefaultValue = value; OnPropertyChanged("DefaultValue"); } } }

    private String _Description;
    /// <summary>描述</summary>
    [DisplayName("描述")]
    [Description("描述")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Description", "描述", "")]
    public String Description { get => _Description; set { if (OnPropertyChanging("Description", value)) { _Description = value; OnPropertyChanged("Description"); } } }

    private Int32 _Sorting;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sorting", "排序", "")]
    public Int32 Sorting { get => _Sorting; set { if (OnPropertyChanging("Sorting", value)) { _Sorting = value; OnPropertyChanged("Sorting"); } } }

    private Boolean _Enable;
    /// <summary>启用</summary>
    [DisplayName("启用")]
    [Description("启用")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Enable", "启用", "")]
    public Boolean Enable { get => _Enable; set { if (OnPropertyChanging("Enable", value)) { _Enable = value; OnPropertyChanged("Enable"); } } }

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
    public void Copy(ICmsFormField model)
    {
        ID = model.ID;
        FormID = model.FormID;
        Name = model.Name;
        FieldType = model.FieldType;
        DisplayName = model.DisplayName;
        Length = model.Length;
        DefaultValue = model.DefaultValue;
        Description = model.Description;
        Sorting = model.Sorting;
        Enable = model.Enable;
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
            "FormID" => _FormID,
            "Name" => _Name,
            "FieldType" => _FieldType,
            "DisplayName" => _DisplayName,
            "Length" => _Length,
            "DefaultValue" => _DefaultValue,
            "Description" => _Description,
            "Sorting" => _Sorting,
            "Enable" => _Enable,
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
                case "FormID": _FormID = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "FieldType": _FieldType = (LeoChen.Cms.Data.CmsItemType)value.ToInt(); break;
                case "DisplayName": _DisplayName = Convert.ToString(value); break;
                case "Length": _Length = value.ToInt(); break;
                case "DefaultValue": _DefaultValue = Convert.ToString(value); break;
                case "Description": _Description = Convert.ToString(value); break;
                case "Sorting": _Sorting = value.ToInt(); break;
                case "Enable": _Enable = value.ToBoolean(); break;
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
    /// <summary>表单编号</summary>
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public CmsForm Form => Extends.Get(nameof(Form), k => CmsForm.FindByID(FormID));

    /// <summary>表单编号</summary>
    [Map(nameof(FormID), typeof(CmsForm), "ID")]
    public String FormName => Form?.Name;

    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static CmsFormField FindByID(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.ID == id);
    }

    /// <summary>根据字段名查找</summary>
    /// <param name="name">字段名</param>
    /// <returns>实体对象</returns>
    public static CmsFormField FindByName(String name)
    {
        if (name.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Name.EqualIgnoreCase(name));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(name) as CmsFormField;

        //return Find(_.Name == name);
    }

    /// <summary>根据表单编号查找</summary>
    /// <param name="formId">表单编号</param>
    /// <returns>实体列表</returns>
    public static IList<CmsFormField> FindAllByFormID(Int32 formId)
    {
        if (formId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.FormID == formId);

        return FindAll(_.FormID == formId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="formId">表单编号</param>
    /// <param name="enable">启用</param>
    /// <param name="fieldType">字段类型</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<CmsFormField> Search(Int32 formId, Boolean? enable, LeoChen.Cms.Data.CmsItemType fieldType, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (formId >= 0) exp &= _.FormID == formId;
        if (enable != null) exp &= _.Enable == enable;
        if (fieldType >= 0) exp &= _.FieldType == fieldType;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得自定义表单字段字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field ID = FindByName("ID");

        /// <summary>表单编号</summary>
        public static readonly Field FormID = FindByName("FormID");

        /// <summary>字段名</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>字段类型</summary>
        public static readonly Field FieldType = FindByName("FieldType");

        /// <summary>显示名</summary>
        public static readonly Field DisplayName = FindByName("DisplayName");

        /// <summary>长度</summary>
        public static readonly Field Length = FindByName("Length");

        /// <summary>默认值</summary>
        public static readonly Field DefaultValue = FindByName("DefaultValue");

        /// <summary>描述</summary>
        public static readonly Field Description = FindByName("Description");

        /// <summary>排序</summary>
        public static readonly Field Sorting = FindByName("Sorting");

        /// <summary>启用</summary>
        public static readonly Field Enable = FindByName("Enable");

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

    /// <summary>取得自定义表单字段字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String ID = "ID";

        /// <summary>表单编号</summary>
        public const String FormID = "FormID";

        /// <summary>字段名</summary>
        public const String Name = "Name";

        /// <summary>字段类型</summary>
        public const String FieldType = "FieldType";

        /// <summary>显示名</summary>
        public const String DisplayName = "DisplayName";

        /// <summary>长度</summary>
        public const String Length = "Length";

        /// <summary>默认值</summary>
        public const String DefaultValue = "DefaultValue";

        /// <summary>描述</summary>
        public const String Description = "Description";

        /// <summary>排序</summary>
        public const String Sorting = "Sorting";

        /// <summary>启用</summary>
        public const String Enable = "Enable";

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
