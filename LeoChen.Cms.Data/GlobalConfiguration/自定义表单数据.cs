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

/// <summary>自定义表单数据</summary>
[Serializable]
[DataObject]
[Description("自定义表单数据")]
[BindIndex("IX_CmsExtForm_FormID", false, "FormID")]
[BindTable("CmsExtForm", Description = "自定义表单数据", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class CmsExtForm : ICmsExtForm, IEntity<ICmsExtForm>
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
    /// <summary>表单名</summary>
    [DisplayName("表单名")]
    [Description("表单名")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("FormID", "表单名", "")]
    public Int32 FormID { get => _FormID; set { if (OnPropertyChanging("FormID", value)) { _FormID = value; OnPropertyChanged("FormID"); } } }

    private String _FormValue;
    /// <summary>表单内容</summary>
    [DisplayName("表单内容")]
    [Description("表单内容")]
    [DataObjectField(false, false, true, 2000)]
    [BindColumn("FormValue", "表单内容", "")]
    public String FormValue { get => _FormValue; set { if (OnPropertyChanging("FormValue", value)) { _FormValue = value; OnPropertyChanged("FormValue"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ICmsExtForm model)
    {
        ID = model.ID;
        FormID = model.FormID;
        FormValue = model.FormValue;
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
            "FormValue" => _FormValue,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "ID": _ID = value.ToInt(); break;
                case "FormID": _FormID = value.ToInt(); break;
                case "FormValue": _FormValue = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    /// <summary>表单名</summary>
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public CmsForm Form => Extends.Get(nameof(Form), k => CmsForm.FindByID(FormID));

    /// <summary>表单名</summary>
    [Map(nameof(FormID), typeof(CmsForm), "ID")]
    public String FormName => Form?.Name;

    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static CmsExtForm FindByID(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.ID == id);
    }

    /// <summary>根据表单名查找</summary>
    /// <param name="formId">表单名</param>
    /// <returns>实体列表</returns>
    public static IList<CmsExtForm> FindAllByFormID(Int32 formId)
    {
        if (formId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.FormID == formId);

        return FindAll(_.FormID == formId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="formId">表单名</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<CmsExtForm> Search(Int32 formId, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (formId >= 0) exp &= _.FormID == formId;
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得自定义表单数据字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field ID = FindByName("ID");

        /// <summary>表单名</summary>
        public static readonly Field FormID = FindByName("FormID");

        /// <summary>表单内容</summary>
        public static readonly Field FormValue = FindByName("FormValue");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得自定义表单数据字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String ID = "ID";

        /// <summary>表单名</summary>
        public const String FormID = "FormID";

        /// <summary>表单内容</summary>
        public const String FormValue = "FormValue";
    }
    #endregion
}
