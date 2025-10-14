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

/// <summary>内容管理表</summary>
[Serializable]
[DataObject]
[Description("内容管理表")]
[BindIndex("IU_CmsContent_Title", true, "Title")]
[BindIndex("IX_CmsContent_AreaID", false, "AreaID")]
[BindIndex("IX_CmsContent_Date", false, "Date")]
[BindIndex("IX_CmsContent_Filename", false, "Filename")]
[BindIndex("IX_CmsContent_ContentSortID", false, "ContentSortID")]
[BindIndex("IX_CmsContent_Sorting", false, "Sorting")]
[BindIndex("IX_CmsContent_Status", false, "Status")]
[BindIndex("IX_CmsContent_ContentSortSubID", false, "ContentSortSubID")]
[BindIndex("IX_CmsContent_IsTop", false, "IsTop")]
[BindIndex("IX_CmsContent_IsRecommend", false, "IsRecommend")]
[BindIndex("IX_CmsContent_IsHeadline", false, "IsHeadline")]
[BindTable("CmsContent", Description = "内容管理表", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class CmsContent : ICmsContent, IEntity<ICmsContent>
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

    private Int32 _ContentSortID;
    /// <summary>分类代码</summary>
    [DisplayName("分类代码")]
    [Description("分类代码")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ContentSortID", "分类代码", "")]
    public Int32 ContentSortID { get => _ContentSortID; set { if (OnPropertyChanging("ContentSortID", value)) { _ContentSortID = value; OnPropertyChanged("ContentSortID"); } } }

    private Int32 _ContentSortSubID;
    /// <summary>子分类代码</summary>
    [DisplayName("子分类代码")]
    [Description("子分类代码")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ContentSortSubID", "子分类代码", "")]
    public Int32 ContentSortSubID { get => _ContentSortSubID; set { if (OnPropertyChanging("ContentSortSubID", value)) { _ContentSortSubID = value; OnPropertyChanged("ContentSortSubID"); } } }

    private String _Title;
    /// <summary>标题</summary>
    [DisplayName("标题")]
    [Description("标题")]
    [DataObjectField(false, false, false, 100)]
    [BindColumn("Title", "标题", "", Master = true)]
    public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

    private String _Titlecolor;
    /// <summary>标题颜色</summary>
    [DisplayName("标题颜色")]
    [Description("标题颜色")]
    [DataObjectField(false, false, true, 7)]
    [BindColumn("Titlecolor", "标题颜色", "")]
    public String Titlecolor { get => _Titlecolor; set { if (OnPropertyChanging("Titlecolor", value)) { _Titlecolor = value; OnPropertyChanged("Titlecolor"); } } }

    private String _Subtitle;
    /// <summary>副标题</summary>
    [DisplayName("副标题")]
    [Description("副标题")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Subtitle", "副标题", "")]
    public String Subtitle { get => _Subtitle; set { if (OnPropertyChanging("Subtitle", value)) { _Subtitle = value; OnPropertyChanged("Subtitle"); } } }

    private String _Filename;
    /// <summary>文件名</summary>
    [DisplayName("文件名")]
    [Description("文件名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Filename", "文件名", "")]
    public String Filename { get => _Filename; set { if (OnPropertyChanging("Filename", value)) { _Filename = value; OnPropertyChanged("Filename"); } } }

    private String _Author;
    /// <summary>作者</summary>
    [DisplayName("作者")]
    [Description("作者")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Author", "作者", "")]
    public String Author { get => _Author; set { if (OnPropertyChanging("Author", value)) { _Author = value; OnPropertyChanged("Author"); } } }

    private String _Source;
    /// <summary>来源</summary>
    [DisplayName("来源")]
    [Description("来源")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Source", "来源", "")]
    public String Source { get => _Source; set { if (OnPropertyChanging("Source", value)) { _Source = value; OnPropertyChanged("Source"); } } }

    private String _Outlink;
    /// <summary>外部链接</summary>
    [DisplayName("外部链接")]
    [Description("外部链接")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Outlink", "外部链接", "")]
    public String Outlink { get => _Outlink; set { if (OnPropertyChanging("Outlink", value)) { _Outlink = value; OnPropertyChanged("Outlink"); } } }

    private DateTime _Date;
    /// <summary>日期</summary>
    [DisplayName("日期")]
    [Description("日期")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("Date", "日期", "")]
    public DateTime Date { get => _Date; set { if (OnPropertyChanging("Date", value)) { _Date = value; OnPropertyChanged("Date"); } } }

    private String _Ico;
    /// <summary>图标</summary>
    [DisplayName("图标")]
    [Description("图标")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Ico", "图标", "")]
    public String Ico { get => _Ico; set { if (OnPropertyChanging("Ico", value)) { _Ico = value; OnPropertyChanged("Ico"); } } }

    private String _Pics;
    /// <summary>图片集</summary>
    [DisplayName("图片集")]
    [Description("图片集")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("Pics", "图片集", "")]
    public String Pics { get => _Pics; set { if (OnPropertyChanging("Pics", value)) { _Pics = value; OnPropertyChanged("Pics"); } } }

    private String _Content;
    /// <summary>内容</summary>
    [DisplayName("内容")]
    [Description("内容")]
    [DataObjectField(false, false, true, 60000)]
    [BindColumn("Content", "内容", "",ItemType = "UeText")]
    public String Content { get => _Content; set { if (OnPropertyChanging("Content", value)) { _Content = value; OnPropertyChanged("Content"); } } }

    private String _Tags;
    /// <summary>标签</summary>
    [DisplayName("标签")]
    [Description("标签")]
    [DataObjectField(false, false, true, 500)]
    [BindColumn("Tags", "标签", "")]
    public String Tags { get => _Tags; set { if (OnPropertyChanging("Tags", value)) { _Tags = value; OnPropertyChanged("Tags"); } } }

    private String _Enclosure;
    /// <summary>附件</summary>
    [DisplayName("附件")]
    [Description("附件")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Enclosure", "附件", "")]
    public String Enclosure { get => _Enclosure; set { if (OnPropertyChanging("Enclosure", value)) { _Enclosure = value; OnPropertyChanged("Enclosure"); } } }

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

    private Int32 _Sorting;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sorting", "排序", "")]
    public Int32 Sorting { get => _Sorting; set { if (OnPropertyChanging("Sorting", value)) { _Sorting = value; OnPropertyChanged("Sorting"); } } }

    private Boolean _Status;
    /// <summary>状态</summary>
    [DisplayName("状态")]
    [Description("状态")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Status", "状态", "")]
    public Boolean Status { get => _Status; set { if (OnPropertyChanging("Status", value)) { _Status = value; OnPropertyChanged("Status"); } } }

    private Boolean _IsTop;
    /// <summary>是否置顶</summary>
    [DisplayName("是否置顶")]
    [Description("是否置顶")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsTop", "是否置顶", "")]
    public Boolean IsTop { get => _IsTop; set { if (OnPropertyChanging("IsTop", value)) { _IsTop = value; OnPropertyChanged("IsTop"); } } }

    private Boolean _IsRecommend;
    /// <summary>是否推荐</summary>
    [DisplayName("是否推荐")]
    [Description("是否推荐")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsRecommend", "是否推荐", "")]
    public Boolean IsRecommend { get => _IsRecommend; set { if (OnPropertyChanging("IsRecommend", value)) { _IsRecommend = value; OnPropertyChanged("IsRecommend"); } } }

    private Boolean _IsHeadline;
    /// <summary>是否头条</summary>
    [DisplayName("是否头条")]
    [Description("是否头条")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsHeadline", "是否头条", "")]
    public Boolean IsHeadline { get => _IsHeadline; set { if (OnPropertyChanging("IsHeadline", value)) { _IsHeadline = value; OnPropertyChanged("IsHeadline"); } } }

    private Int32 _Visits;
    /// <summary>访问数</summary>
    [DisplayName("访问数")]
    [Description("访问数")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Visits", "访问数", "")]
    public Int32 Visits { get => _Visits; set { if (OnPropertyChanging("Visits", value)) { _Visits = value; OnPropertyChanged("Visits"); } } }

    private Int32 _Likes;
    /// <summary>点赞数</summary>
    [DisplayName("点赞数")]
    [Description("点赞数")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Likes", "点赞数", "")]
    public Int32 Likes { get => _Likes; set { if (OnPropertyChanging("Likes", value)) { _Likes = value; OnPropertyChanged("Likes"); } } }

    private Int32 _Oppose;
    /// <summary>反对数</summary>
    [DisplayName("反对数")]
    [Description("反对数")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Oppose", "反对数", "")]
    public Int32 Oppose { get => _Oppose; set { if (OnPropertyChanging("Oppose", value)) { _Oppose = value; OnPropertyChanged("Oppose"); } } }

    private String _Picstitle;
    /// <summary>图片标题</summary>
    [DisplayName("图片标题")]
    [Description("图片标题")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("Picstitle", "图片标题", "")]
    public String Picstitle { get => _Picstitle; set { if (OnPropertyChanging("Picstitle", value)) { _Picstitle = value; OnPropertyChanged("Picstitle"); } } }

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
    public void Copy(ICmsContent model)
    {
        ID = model.ID;
        AreaID = model.AreaID;
        ContentSortID = model.ContentSortID;
        ContentSortSubID = model.ContentSortSubID;
        Title = model.Title;
        Titlecolor = model.Titlecolor;
        Subtitle = model.Subtitle;
        Filename = model.Filename;
        Author = model.Author;
        Source = model.Source;
        Outlink = model.Outlink;
        Date = model.Date;
        Ico = model.Ico;
        Pics = model.Pics;
        Content = model.Content;
        Tags = model.Tags;
        Enclosure = model.Enclosure;
        Keywords = model.Keywords;
        Description = model.Description;
        Sorting = model.Sorting;
        Status = model.Status;
        IsTop = model.IsTop;
        IsRecommend = model.IsRecommend;
        IsHeadline = model.IsHeadline;
        Visits = model.Visits;
        Likes = model.Likes;
        Oppose = model.Oppose;
        Picstitle = model.Picstitle;
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
            "ContentSortID" => _ContentSortID,
            "ContentSortSubID" => _ContentSortSubID,
            "Title" => _Title,
            "Titlecolor" => _Titlecolor,
            "Subtitle" => _Subtitle,
            "Filename" => _Filename,
            "Author" => _Author,
            "Source" => _Source,
            "Outlink" => _Outlink,
            "Date" => _Date,
            "Ico" => _Ico,
            "Pics" => _Pics,
            "Content" => _Content,
            "Tags" => _Tags,
            "Enclosure" => _Enclosure,
            "Keywords" => _Keywords,
            "Description" => _Description,
            "Sorting" => _Sorting,
            "Status" => _Status,
            "IsTop" => _IsTop,
            "IsRecommend" => _IsRecommend,
            "IsHeadline" => _IsHeadline,
            "Visits" => _Visits,
            "Likes" => _Likes,
            "Oppose" => _Oppose,
            "Picstitle" => _Picstitle,
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
                case "ContentSortID": _ContentSortID = value.ToInt(); break;
                case "ContentSortSubID": _ContentSortSubID = value.ToInt(); break;
                case "Title": _Title = Convert.ToString(value); break;
                case "Titlecolor": _Titlecolor = Convert.ToString(value); break;
                case "Subtitle": _Subtitle = Convert.ToString(value); break;
                case "Filename": _Filename = Convert.ToString(value); break;
                case "Author": _Author = Convert.ToString(value); break;
                case "Source": _Source = Convert.ToString(value); break;
                case "Outlink": _Outlink = Convert.ToString(value); break;
                case "Date": _Date = value.ToDateTime(); break;
                case "Ico": _Ico = Convert.ToString(value); break;
                case "Pics": _Pics = Convert.ToString(value); break;
                case "Content": _Content = Convert.ToString(value); break;
                case "Tags": _Tags = Convert.ToString(value); break;
                case "Enclosure": _Enclosure = Convert.ToString(value); break;
                case "Keywords": _Keywords = Convert.ToString(value); break;
                case "Description": _Description = Convert.ToString(value); break;
                case "Sorting": _Sorting = value.ToInt(); break;
                case "Status": _Status = value.ToBoolean(); break;
                case "IsTop": _IsTop = value.ToBoolean(); break;
                case "IsRecommend": _IsRecommend = value.ToBoolean(); break;
                case "IsHeadline": _IsHeadline = value.ToBoolean(); break;
                case "Visits": _Visits = value.ToInt(); break;
                case "Likes": _Likes = value.ToInt(); break;
                case "Oppose": _Oppose = value.ToInt(); break;
                case "Picstitle": _Picstitle = Convert.ToString(value); break;
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

    /// <summary>分类代码</summary>
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public CmsContent_Sort ContentSort => Extends.Get(nameof(ContentSort), k => CmsContent_Sort.FindByID(ContentSortID));

    /// <summary>分类代码</summary>
    [Map(nameof(ContentSortID), typeof(CmsContent_Sort), "ID")]
    public String ContentSortName => ContentSort?.Name;

    /// <summary>子分类代码</summary>
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public CmsContent_Sort ContentSortSub => Extends.Get(nameof(ContentSortSub), k => CmsContent_Sort.FindByID(ContentSortSubID));

    /// <summary>子分类代码</summary>
    [Map(nameof(ContentSortSubID), typeof(CmsContent_Sort), "ID")]
    public String ContentSortSubName => ContentSortSub?.Name;

    #endregion

    #region 扩展查询
    /// <summary>根据主键ID查找</summary>
    /// <param name="id">主键ID</param>
    /// <returns>实体对象</returns>
    public static CmsContent FindByID(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.ID == id);
    }

    /// <summary>根据标题查找</summary>
    /// <param name="title">标题</param>
    /// <returns>实体对象</returns>
    public static CmsContent FindByTitle(String title)
    {
        if (title.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Title.EqualIgnoreCase(title));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(title) as CmsContent;

        //return Find(_.Title == title);
    }

    /// <summary>根据区域代码查找</summary>
    /// <param name="areaId">区域代码</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent> FindAllByAreaID(Int32 areaId)
    {
        if (areaId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.AreaID == areaId);

        return FindAll(_.AreaID == areaId);
    }

    /// <summary>根据文件名查找</summary>
    /// <param name="filename">文件名</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent> FindAllByFilename(String filename)
    {
        if (filename.IsNullOrEmpty()) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Filename.EqualIgnoreCase(filename));

        return FindAll(_.Filename == filename);
    }

    /// <summary>根据分类代码查找</summary>
    /// <param name="contentSortId">分类代码</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent> FindAllByContentSortID(Int32 contentSortId)
    {
        if (contentSortId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ContentSortID == contentSortId);

        return FindAll(_.ContentSortID == contentSortId);
    }

    /// <summary>根据排序查找</summary>
    /// <param name="sorting">排序</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent> FindAllBySorting(Int32 sorting)
    {
        if (sorting < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Sorting == sorting);

        return FindAll(_.Sorting == sorting);
    }

    /// <summary>根据子分类代码查找</summary>
    /// <param name="contentSortSubId">子分类代码</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent> FindAllByContentSortSubID(Int32 contentSortSubId)
    {
        if (contentSortSubId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ContentSortSubID == contentSortSubId);

        return FindAll(_.ContentSortSubID == contentSortSubId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="areaId">区域代码</param>
    /// <param name="contentSortId">分类代码</param>
    /// <param name="contentSortSubId">子分类代码</param>
    /// <param name="filename">文件名</param>
    /// <param name="sorting">排序</param>
    /// <param name="status">状态</param>
    /// <param name="isTop">是否置顶</param>
    /// <param name="isRecommend">是否推荐</param>
    /// <param name="isHeadline">是否头条</param>
    /// <param name="start">日期开始</param>
    /// <param name="end">日期结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<CmsContent> Search(Int32 areaId, Int32 contentSortId, Int32 contentSortSubId, String filename, Int32 sorting, Boolean? status, Boolean? isTop, Boolean? isRecommend, Boolean? isHeadline, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (areaId >= 0) exp &= _.AreaID == areaId;
        if (contentSortId >= 0) exp &= _.ContentSortID == contentSortId;
        if (contentSortSubId >= 0) exp &= _.ContentSortSubID == contentSortSubId;
        if (!filename.IsNullOrEmpty()) exp &= _.Filename == filename;
        if (sorting >= 0) exp &= _.Sorting == sorting;
        if (status != null) exp &= _.Status == status;
        if (isTop != null) exp &= _.IsTop == isTop;
        if (isRecommend != null) exp &= _.IsRecommend == isRecommend;
        if (isHeadline != null) exp &= _.IsHeadline == isHeadline;
        exp &= _.Date.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得内容管理表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>主键ID</summary>
        public static readonly Field ID = FindByName("ID");

        /// <summary>区域代码</summary>
        public static readonly Field AreaID = FindByName("AreaID");

        /// <summary>分类代码</summary>
        public static readonly Field ContentSortID = FindByName("ContentSortID");

        /// <summary>子分类代码</summary>
        public static readonly Field ContentSortSubID = FindByName("ContentSortSubID");

        /// <summary>标题</summary>
        public static readonly Field Title = FindByName("Title");

        /// <summary>标题颜色</summary>
        public static readonly Field Titlecolor = FindByName("Titlecolor");

        /// <summary>副标题</summary>
        public static readonly Field Subtitle = FindByName("Subtitle");

        /// <summary>文件名</summary>
        public static readonly Field Filename = FindByName("Filename");

        /// <summary>作者</summary>
        public static readonly Field Author = FindByName("Author");

        /// <summary>来源</summary>
        public static readonly Field Source = FindByName("Source");

        /// <summary>外部链接</summary>
        public static readonly Field Outlink = FindByName("Outlink");

        /// <summary>日期</summary>
        public static readonly Field Date = FindByName("Date");

        /// <summary>图标</summary>
        public static readonly Field Ico = FindByName("Ico");

        /// <summary>图片集</summary>
        public static readonly Field Pics = FindByName("Pics");

        /// <summary>内容</summary>
        public static readonly Field Content = FindByName("Content");

        /// <summary>标签</summary>
        public static readonly Field Tags = FindByName("Tags");

        /// <summary>附件</summary>
        public static readonly Field Enclosure = FindByName("Enclosure");

        /// <summary>关键词</summary>
        public static readonly Field Keywords = FindByName("Keywords");

        /// <summary>描述</summary>
        public static readonly Field Description = FindByName("Description");

        /// <summary>排序</summary>
        public static readonly Field Sorting = FindByName("Sorting");

        /// <summary>状态</summary>
        public static readonly Field Status = FindByName("Status");

        /// <summary>是否置顶</summary>
        public static readonly Field IsTop = FindByName("IsTop");

        /// <summary>是否推荐</summary>
        public static readonly Field IsRecommend = FindByName("IsRecommend");

        /// <summary>是否头条</summary>
        public static readonly Field IsHeadline = FindByName("IsHeadline");

        /// <summary>访问数</summary>
        public static readonly Field Visits = FindByName("Visits");

        /// <summary>点赞数</summary>
        public static readonly Field Likes = FindByName("Likes");

        /// <summary>反对数</summary>
        public static readonly Field Oppose = FindByName("Oppose");

        /// <summary>图片标题</summary>
        public static readonly Field Picstitle = FindByName("Picstitle");

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

    /// <summary>取得内容管理表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>主键ID</summary>
        public const String ID = "ID";

        /// <summary>区域代码</summary>
        public const String AreaID = "AreaID";

        /// <summary>分类代码</summary>
        public const String ContentSortID = "ContentSortID";

        /// <summary>子分类代码</summary>
        public const String ContentSortSubID = "ContentSortSubID";

        /// <summary>标题</summary>
        public const String Title = "Title";

        /// <summary>标题颜色</summary>
        public const String Titlecolor = "Titlecolor";

        /// <summary>副标题</summary>
        public const String Subtitle = "Subtitle";

        /// <summary>文件名</summary>
        public const String Filename = "Filename";

        /// <summary>作者</summary>
        public const String Author = "Author";

        /// <summary>来源</summary>
        public const String Source = "Source";

        /// <summary>外部链接</summary>
        public const String Outlink = "Outlink";

        /// <summary>日期</summary>
        public const String Date = "Date";

        /// <summary>图标</summary>
        public const String Ico = "Ico";

        /// <summary>图片集</summary>
        public const String Pics = "Pics";

        /// <summary>内容</summary>
        public const String Content = "Content";

        /// <summary>标签</summary>
        public const String Tags = "Tags";

        /// <summary>附件</summary>
        public const String Enclosure = "Enclosure";

        /// <summary>关键词</summary>
        public const String Keywords = "Keywords";

        /// <summary>描述</summary>
        public const String Description = "Description";

        /// <summary>排序</summary>
        public const String Sorting = "Sorting";

        /// <summary>状态</summary>
        public const String Status = "Status";

        /// <summary>是否置顶</summary>
        public const String IsTop = "IsTop";

        /// <summary>是否推荐</summary>
        public const String IsRecommend = "IsRecommend";

        /// <summary>是否头条</summary>
        public const String IsHeadline = "IsHeadline";

        /// <summary>访问数</summary>
        public const String Visits = "Visits";

        /// <summary>点赞数</summary>
        public const String Likes = "Likes";

        /// <summary>反对数</summary>
        public const String Oppose = "Oppose";

        /// <summary>图片标题</summary>
        public const String Picstitle = "Picstitle";

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
