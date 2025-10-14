using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using LeoChen.Cms.Data;
using NewLife.Caching;
using NewLife.Cube.Entity;
using NewLife.Reflection;
using XCode;

namespace LeoChen.Cms.TemplateEngine;

public class TemplateEngineCache
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICache _cache;

    public TemplateEngineCache(ICacheProvider cacheProvider, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _cache = cacheProvider.Cache;
    }

    #region 缓存名称

    /// <summary>区域列表缓存</summary>
    public static string CacheAreaList { get; } = "Cache_AreaList";

    /// <summary>栏目字典缓存</summary>
    public static string CacheContentSortDic { get; } = "Cache_ContentSortDic_";
    /// <summary>UrlName为键名的栏目字典</summary>
    public static string CacheContentSortDicByUrlName { get; } = "Cache_ContentSortDicByUrlName_";

    /// <summary>文件名为键名的栏目字典</summary>
    public static string CacheContentSortDicByFileName { get; } = "Cache_ContentSortDicByFileName_";

    /// <summary>站点信息缓存</summary>
    public static string CacheSiteValue { get; } = "Cache_Site_";

    /// <summary>公司信息缓存</summary>
    public static string CacheCompanyValue { get; } = "Cache_Company_";

    /// <summary>模板目录路径</summary>
    public static string CacheTemplatePathValue { get; } = "Cache_TemplatePath_";

    /// <summary>模板文件缓存</summary>
    public static string CacheTemplateValue { get; } = "Cache_Template_";

    #endregion

    #region 数据管理

    /// <summary>
    /// 获取区域列表
    /// </summary>
    /// <param name="arealist">区域列表</param>
    /// <returns></returns>
    public Boolean GetAreaList(out Dictionary<String, CmsArea> arealist)
    {
        return GetCacheData(CacheAreaList, d =>
        {
            var cmsAreas = new Dictionary<String, CmsArea>(StringComparer.OrdinalIgnoreCase);
            var list = CmsArea.FindAll();
            foreach (var cmsArea in list)
            {
                if (cmsArea.Domain.IsNullOrEmpty()) continue;
                string host;
                try
                {
                    var uri = new Uri(cmsArea.Domain);
                    host = uri.Host;
                }
                catch (UriFormatException)
                {
                    host = cmsArea.Domain;
                }

                cmsAreas.Add(host, cmsArea);
            }

            return cmsAreas;
        }, out arealist);
    }

    /// <summary>
    /// 获取站点信息
    /// </summary>
    /// <param name="areaid">区域ID</param>
    /// <param name="cmsSite">站点信息</param>
    /// <returns></returns>
    public Boolean GetSite(int areaid, out CmsSite cmsSite)
    {
        return GetCacheData(
            CacheSiteValue + areaid,
            d => CmsSite.FindByAreaID(areaid),
            out cmsSite);
    }

    public Boolean SetSite(int areaid, CmsSite cmsSite)
    {
        return GetCacheData(
            CacheSiteValue + areaid,
            d => CmsSite.FindByAreaID(areaid),
            out cmsSite);
    }


    /// <summary>
    /// 获取模板路径
    /// </summary>
    /// <param name="areaid">区域ID</param>
    /// <param name="templatePath">模板路径</param>
    /// <returns></returns>
    public Boolean GetTemplatePath(int areaid, out String templatePath)
    {
        return GetCacheData(
            CacheTemplatePathValue + areaid,
            d =>
            {
                string path;
                if (!GetSite(areaid, out var cmsSite)) return null;
                if (cmsSite.Theme.IsNullOrEmpty()) path = "Template/Default/";
                else path = $"Template{cmsSite.Theme.EnsureStart("/").EnsureEnd("/")}";
                path.GetFullPath().EnsureDirectory(false);
                return path;
            },
            out templatePath);
    }

    /// <summary>
    /// 获取UrlName为键名的栏目字典  
    /// </summary>
    /// <param name="areaid">区域ID</param>
    /// <param name="cmsContentSortDicByUrlName">内容分类字典</param>
    /// <returns></returns>
    public Boolean GetContentSortDicByUrlName(int areaid, out Dictionary<String, CmsContent_Sort> cmsContentSortDicByUrlName)
    {
        return GetCacheData(
            CacheContentSortDicByUrlName + areaid,
            d => CmsContent_Sort.FindAllByAreaID(areaid)
                .Where(contentSort => !string.IsNullOrEmpty(contentSort.UrlName))
                .ToDictionary(contentSort => contentSort.UrlName, contentSort => contentSort,
                    StringComparer.OrdinalIgnoreCase),
            out cmsContentSortDicByUrlName);
    }
    /// <summary>
    /// 获取FileName为键名的栏目字典
    /// </summary>
    /// <param name="areaid">区域ID</param>
    /// <param name="cmsContentSortDicByUrlName">内容分类字典</param>
    /// <returns></returns>
    public Boolean GetContentSortDicByFileName(int areaid, out Dictionary<String, CmsContent_Sort> cmsContentSortDicByFileName)
    {
        return GetCacheData(
            CacheContentSortDicByFileName + areaid,
            d => CmsContent_Sort.FindAllByAreaID(areaid)
                .Where(contentSort => !string.IsNullOrEmpty(contentSort.Filename))
                .ToDictionary(contentSort => contentSort.Filename.EnsureEnd(".html"), contentSort => contentSort,
                    StringComparer.OrdinalIgnoreCase),
            out cmsContentSortDicByFileName);
    }
    
    /// <summary>
    /// 栏目字典
    /// </summary>
    /// <param name="areaid">区域ID</param>
    /// <param name="cmsContentSortDic">内容分类字典</param>
    /// <returns></returns>
    public Boolean GetContentSortDic(int areaid, out Dictionary<Int32, CmsContent_Sort> cmsContentSortDic)
    {
        return GetCacheData(
            CacheContentSortDic + areaid,
            d => CmsContent_Sort.FindAllByAreaID(areaid).ToDictionary(contentSort => contentSort.ID, contentSort => contentSort), 
            out cmsContentSortDic);
    }

    /// <summary>
    /// 获取缓存数据
    /// </summary>
    public Boolean GetCacheData<T>(String key, Func<string, T> dataProvider, [MaybeNullWhen(false)] out T value)
    {
        if (_cache.TryGetValue<T>(key, out value)) return true;

        if (dataProvider == null) return false;

        value = dataProvider(key);
        if (value == null) return false;
        if (value is string str && str.IsNullOrEmpty()) return false;
        if (value is IEnumerable enumerable && !enumerable.Cast<object>().Any()) return false;
        _cache.Set(key, value, 0);
        return true;
    }

    public Boolean GetCacheData<T>(String key, [MaybeNullWhen(false)] out T value)
    {
        if (_cache.TryGetValue<T>(key, out value)) return true;
        return false;
    }

    /// <summary>
    /// 获取数据（优先级：缓存数据 > 全局配置 > 请求/时间数据）
    /// </summary>
    private object GetData(String key)
    {
        if (string.IsNullOrEmpty(key)) return null;

        // // 1. 从专门的缓存数据提供方法获取
        // var cacheData = _dataProvider(key);
        // if (cacheData != null)
        //     return cacheData;
        //
        // // 2. 再取全局配置（如site、config）
        // if (_globalConfig.TryGetValue(key, out var globalVal))
        //     return globalVal;

        // 3. 处理多级数据（如site.name、config.webname）
        if (key.Contains('.'))
        {
            var parts = key.Split('.', 2);
            var parentKey = parts[0];
            var childKey = parts[1];
            var parentData = GetData(parentKey);
            if (parentData != null)
                return GetPropertyValue(parentData, childKey);
            return null;
        }

        // 4. 处理请求相关数据（如request.ip、request.url）
        if (key.StartsWith("request.", StringComparison.OrdinalIgnoreCase))
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx == null) return null;

            var subKey = key.Substring(8).ToLower();
            return subKey switch
            {
                "ip" => ctx.Connection.RemoteIpAddress?.ToString(),
                "url" => ctx.Request.Path + ctx.Request.QueryString,
                "path" => ctx.Request.Path,
                "query" => ctx.Request.QueryString.ToString(),
                "useragent" => ctx.Request.Headers["User-Agent"].ToString(),
                "referer" => ctx.Request.Headers["Referer"].ToString(),
                _ => null
            };
        }

        // 5. 处理时间相关数据（如time.now、time.year）
        if (key.StartsWith("time.", StringComparison.OrdinalIgnoreCase))
        {
            var now = DateTime.Now;
            var subKey = key.Substring(5).ToLower();
            return subKey switch
            {
                "now" => now.ToString("yyyy-MM-dd HH:mm:ss"),
                "date" => now.ToString("yyyy-MM-dd"),
                "time" => now.ToString("HH:mm:ss"),
                "year" => now.Year,
                "month" => now.Month.ToString("00"),
                "day" => now.Day.ToString("00"),
                "week" => now.DayOfWeek.ToString(),
                "weeknum" => now.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek,
                _ => null
            };
        }

        return null;
    }

    /// <summary>
    /// 获取对象属性值
    /// </summary>
    private static object GetPropertyValue(object obj, string propertyName)
    {
        if (obj == null || string.IsNullOrEmpty(propertyName)) return null;

        // 处理XCode实体（IEntity）
        if (obj is IEntity entity)
            return entity.GetValue(propertyName);

        // 处理字典（IDictionary）
        if (obj is IDictionary dict && dict.Contains(propertyName))
            return dict[propertyName];

        // 处理匿名对象/POCO类
        var prop = obj.GetType().GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        return prop?.GetValue(obj);
    }

    #endregion
}