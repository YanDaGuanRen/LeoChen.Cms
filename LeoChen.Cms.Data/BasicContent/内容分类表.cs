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

/// <summary>内容分类表</summary>
[Serializable]
[DataObject]
[Description("内容分类表")]
[BindIndex("IX_CmsContent_Sort_AreaID", false, "AreaID")]
[BindIndex("IX_CmsContent_Sort_PID", false, "PID")]
[BindIndex("IX_CmsContent_Sort_AreaID_PID", false, "AreaID,PID")]
[BindIndex("IX_CmsContent_Sort_ModelID", false, "ModelID")]
[BindIndex("IX_CmsContent_Sort_Sorting", false, "Sorting")]
[BindIndex("IU_CmsContent_Sort_Name", true, "Name")]
[BindTable("CmsContent_Sort", Description = "内容分类表", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class CmsContent_Sort : ICmsContent_Sort, IEntity<ICmsContent_Sort>
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

    private Int32 _Pid;
    /// <summary>父级代码</summary>
    [DisplayName("父级代码")]
    [Description("父级代码")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Pid", "父级代码", "")]
    public Int32 Pid { get => _Pid; set { if (OnPropertyChanging("Pid", value)) { _Pid = value; OnPropertyChanged("Pid"); } } }

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
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Name", "名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _Subname;
    /// <summary>副名称</summary>
    [DisplayName("副名称")]
    [Description("副名称")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Subname", "副名称", "")]
    public String Subname { get => _Subname; set { if (OnPropertyChanging("Subname", value)) { _Subname = value; OnPropertyChanged("Subname"); } } }

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

    private String _Outlink;
    /// <summary>外部链接</summary>
    [DisplayName("外部链接")]
    [Description("外部链接")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Outlink", "外部链接", "")]
    public String Outlink { get => _Outlink; set { if (OnPropertyChanging("Outlink", value)) { _Outlink = value; OnPropertyChanged("Outlink"); } } }

    private String _Ico;
    /// <summary>图标</summary>
    [DisplayName("图标")]
    [Description("图标")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Ico", "图标", "")]
    public String Ico { get => _Ico; set { if (OnPropertyChanging("Ico", value)) { _Ico = value; OnPropertyChanged("Ico"); } } }

    private String _Pic;
    /// <summary>图片</summary>
    [DisplayName("图片")]
    [Description("图片")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Pic", "图片", "")]
    public String Pic { get => _Pic; set { if (OnPropertyChanging("Pic", value)) { _Pic = value; OnPropertyChanged("Pic"); } } }

    private String _Title;
    /// <summary>标题</summary>
    [DisplayName("标题")]
    [Description("标题")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Title", "标题", "")]
    public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

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

    private String _Filename;
    /// <summary>文件名</summary>
    [DisplayName("文件名")]
    [Description("文件名")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Filename", "文件名", "")]
    public String Filename { get => _Filename; set { if (OnPropertyChanging("Filename", value)) { _Filename = value; OnPropertyChanged("Filename"); } } }

    private Int32 _Sorting;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sorting", "排序", "")]
    public Int32 Sorting { get => _Sorting; set { if (OnPropertyChanging("Sorting", value)) { _Sorting = value; OnPropertyChanged("Sorting"); } } }

    private String _Def1;
    /// <summary>预留字段1</summary>
    [Category("扩展信息")]
    [DisplayName("预留字段1")]
    [Description("预留字段1")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("Def1", "预留字段1", "")]
    public String Def1 { get => _Def1; set { if (OnPropertyChanging("Def1", value)) { _Def1 = value; OnPropertyChanged("Def1"); } } }

    private String _Def2;
    /// <summary>预留字段2</summary>
    [Category("扩展信息")]
    [DisplayName("预留字段2")]
    [Description("预留字段2")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("Def2", "预留字段2", "")]
    public String Def2 { get => _Def2; set { if (OnPropertyChanging("Def2", value)) { _Def2 = value; OnPropertyChanged("Def2"); } } }

    private String _Def3;
    /// <summary>预留字段3</summary>
    [Category("扩展信息")]
    [DisplayName("预留字段3")]
    [Description("预留字段3")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("Def3", "预留字段3", "")]
    public String Def3 { get => _Def3; set { if (OnPropertyChanging("Def3", value)) { _Def3 = value; OnPropertyChanged("Def3"); } } }

    private String _Def4;
    /// <summary>预留字段1</summary>
    [Category("扩展信息")]
    [DisplayName("预留字段1")]
    [Description("预留字段1")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("Def4", "预留字段1", "")]
    public String Def4 { get => _Def4; set { if (OnPropertyChanging("Def4", value)) { _Def4 = value; OnPropertyChanged("Def4"); } } }

    private String _Def5;
    /// <summary>预留字段2</summary>
    [Category("扩展信息")]
    [DisplayName("预留字段2")]
    [Description("预留字段2")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("Def5", "预留字段2", "")]
    public String Def5 { get => _Def5; set { if (OnPropertyChanging("Def5", value)) { _Def5 = value; OnPropertyChanged("Def5"); } } }

    private String _Def6;
    /// <summary>预留字段3</summary>
    [Category("扩展信息")]
    [DisplayName("预留字段3")]
    [Description("预留字段3")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("Def6", "预留字段3", "")]
    public String Def6 { get => _Def6; set { if (OnPropertyChanging("Def6", value)) { _Def6 = value; OnPropertyChanged("Def6"); } } }

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
    public void Copy(ICmsContent_Sort model)
    {
        ID = model.ID;
        AreaID = model.AreaID;
        Pid = model.Pid;
        ModelID = model.ModelID;
        Name = model.Name;
        Subname = model.Subname;
        ListTpl = model.ListTpl;
        ContentTpl = model.ContentTpl;
        Status = model.Status;
        Outlink = model.Outlink;
        Ico = model.Ico;
        Pic = model.Pic;
        Title = model.Title;
        Keywords = model.Keywords;
        Description = model.Description;
        Filename = model.Filename;
        Sorting = model.Sorting;
        Def1 = model.Def1;
        Def2 = model.Def2;
        Def3 = model.Def3;
        Def4 = model.Def4;
        Def5 = model.Def5;
        Def6 = model.Def6;
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
            "Pid" => _Pid,
            "ModelID" => _ModelID,
            "Name" => _Name,
            "Subname" => _Subname,
            "ListTpl" => _ListTpl,
            "ContentTpl" => _ContentTpl,
            "Status" => _Status,
            "Outlink" => _Outlink,
            "Ico" => _Ico,
            "Pic" => _Pic,
            "Title" => _Title,
            "Keywords" => _Keywords,
            "Description" => _Description,
            "Filename" => _Filename,
            "Sorting" => _Sorting,
            "Def1" => _Def1,
            "Def2" => _Def2,
            "Def3" => _Def3,
            "Def4" => _Def4,
            "Def5" => _Def5,
            "Def6" => _Def6,
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
                case "Pid": _Pid = value.ToInt(); break;
                case "ModelID": _ModelID = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Subname": _Subname = Convert.ToString(value); break;
                case "ListTpl": _ListTpl = Convert.ToString(value); break;
                case "ContentTpl": _ContentTpl = Convert.ToString(value); break;
                case "Status": _Status = value.ToBoolean(); break;
                case "Outlink": _Outlink = Convert.ToString(value); break;
                case "Ico": _Ico = Convert.ToString(value); break;
                case "Pic": _Pic = Convert.ToString(value); break;
                case "Title": _Title = Convert.ToString(value); break;
                case "Keywords": _Keywords = Convert.ToString(value); break;
                case "Description": _Description = Convert.ToString(value); break;
                case "Filename": _Filename = Convert.ToString(value); break;
                case "Sorting": _Sorting = value.ToInt(); break;
                case "Def1": _Def1 = Convert.ToString(value); break;
                case "Def2": _Def2 = Convert.ToString(value); break;
                case "Def3": _Def3 = Convert.ToString(value); break;
                case "Def4": _Def4 = Convert.ToString(value); break;
                case "Def5": _Def5 = Convert.ToString(value); break;
                case "Def6": _Def6 = Convert.ToString(value); break;
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
    public static CmsContent_Sort FindByID(Int32 id)
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
    /// <returns>实体列表</returns>
    public static IList<CmsContent_Sort> FindAllByAreaID(Int32 areaId)
    {
        if (areaId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.AreaID == areaId);

        return FindAll(_.AreaID == areaId);
    }

    /// <summary>根据父级代码查找</summary>
    /// <param name="pid">父级代码</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent_Sort> FindAllByPid(Int32 pid)
    {
        if (pid < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Pid == pid);

        return FindAll(_.Pid == pid);
    }

    /// <summary>根据区域代码、父级代码查找</summary>
    /// <param name="areaId">区域代码</param>
    /// <param name="pid">父级代码</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent_Sort> FindAllByAreaIDAndPid(Int32 areaId, Int32 pid)
    {
        if (areaId < 0) return [];
        if (pid < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.AreaID == areaId && e.Pid == pid);

        return FindAll(_.AreaID == areaId & _.Pid == pid);
    }

    /// <summary>根据模型代码查找</summary>
    /// <param name="modelId">模型代码</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent_Sort> FindAllByModelID(Int32 modelId)
    {
        if (modelId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ModelID == modelId);

        return FindAll(_.ModelID == modelId);
    }

    /// <summary>根据排序查找</summary>
    /// <param name="sorting">排序</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent_Sort> FindAllBySorting(Int32 sorting)
    {
        if (sorting < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Sorting == sorting);

        return FindAll(_.Sorting == sorting);
    }

    /// <summary>根据名称查找</summary>
    /// <param name="name">名称</param>
    /// <returns>实体对象</returns>
    public static CmsContent_Sort FindByName(String name)
    {
        if (name.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Name.EqualIgnoreCase(name));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(name) as CmsContent_Sort;

        //return Find(_.Name == name);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="areaId">区域代码</param>
    /// <param name="pid">父级代码</param>
    /// <param name="modelId">模型代码</param>
    /// <param name="sorting">排序</param>
    /// <param name="status">状态</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent_Sort> Search(Int32 areaId, Int32 pid, Int32 modelId, Int32 sorting, Boolean? status, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (areaId >= 0) exp &= _.AreaID == areaId;
        if (pid >= 0) exp &= _.Pid == pid;
        if (modelId >= 0) exp &= _.ModelID == modelId;
        if (sorting >= 0) exp &= _.Sorting == sorting;
        if (status != null) exp &= _.Status == status;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得内容分类表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>主键ID</summary>
        public static readonly Field ID = FindByName("ID");

        /// <summary>区域代码</summary>
        public static readonly Field AreaID = FindByName("AreaID");

        /// <summary>父级代码</summary>
        public static readonly Field Pid = FindByName("Pid");

        /// <summary>模型代码</summary>
        public static readonly Field ModelID = FindByName("ModelID");

        /// <summary>名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>副名称</summary>
        public static readonly Field Subname = FindByName("Subname");

        /// <summary>列表模板</summary>
        public static readonly Field ListTpl = FindByName("ListTpl");

        /// <summary>内容模板</summary>
        public static readonly Field ContentTpl = FindByName("ContentTpl");

        /// <summary>状态</summary>
        public static readonly Field Status = FindByName("Status");

        /// <summary>外部链接</summary>
        public static readonly Field Outlink = FindByName("Outlink");

        /// <summary>图标</summary>
        public static readonly Field Ico = FindByName("Ico");

        /// <summary>图片</summary>
        public static readonly Field Pic = FindByName("Pic");

        /// <summary>标题</summary>
        public static readonly Field Title = FindByName("Title");

        /// <summary>关键词</summary>
        public static readonly Field Keywords = FindByName("Keywords");

        /// <summary>描述</summary>
        public static readonly Field Description = FindByName("Description");

        /// <summary>文件名</summary>
        public static readonly Field Filename = FindByName("Filename");

        /// <summary>排序</summary>
        public static readonly Field Sorting = FindByName("Sorting");

        /// <summary>预留字段1</summary>
        public static readonly Field Def1 = FindByName("Def1");

        /// <summary>预留字段2</summary>
        public static readonly Field Def2 = FindByName("Def2");

        /// <summary>预留字段3</summary>
        public static readonly Field Def3 = FindByName("Def3");

        /// <summary>预留字段1</summary>
        public static readonly Field Def4 = FindByName("Def4");

        /// <summary>预留字段2</summary>
        public static readonly Field Def5 = FindByName("Def5");

        /// <summary>预留字段3</summary>
        public static readonly Field Def6 = FindByName("Def6");

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

    /// <summary>取得内容分类表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>主键ID</summary>
        public const String ID = "ID";

        /// <summary>区域代码</summary>
        public const String AreaID = "AreaID";

        /// <summary>父级代码</summary>
        public const String Pid = "Pid";

        /// <summary>模型代码</summary>
        public const String ModelID = "ModelID";

        /// <summary>名称</summary>
        public const String Name = "Name";

        /// <summary>副名称</summary>
        public const String Subname = "Subname";

        /// <summary>列表模板</summary>
        public const String ListTpl = "ListTpl";

        /// <summary>内容模板</summary>
        public const String ContentTpl = "ContentTpl";

        /// <summary>状态</summary>
        public const String Status = "Status";

        /// <summary>外部链接</summary>
        public const String Outlink = "Outlink";

        /// <summary>图标</summary>
        public const String Ico = "Ico";

        /// <summary>图片</summary>
        public const String Pic = "Pic";

        /// <summary>标题</summary>
        public const String Title = "Title";

        /// <summary>关键词</summary>
        public const String Keywords = "Keywords";

        /// <summary>描述</summary>
        public const String Description = "Description";

        /// <summary>文件名</summary>
        public const String Filename = "Filename";

        /// <summary>排序</summary>
        public const String Sorting = "Sorting";

        /// <summary>预留字段1</summary>
        public const String Def1 = "Def1";

        /// <summary>预留字段2</summary>
        public const String Def2 = "Def2";

        /// <summary>预留字段3</summary>
        public const String Def3 = "Def3";

        /// <summary>预留字段1</summary>
        public const String Def4 = "Def4";

        /// <summary>预留字段2</summary>
        public const String Def5 = "Def5";

        /// <summary>预留字段3</summary>
        public const String Def6 = "Def6";

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
