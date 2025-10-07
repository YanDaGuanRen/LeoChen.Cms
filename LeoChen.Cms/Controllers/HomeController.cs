using LeoChen.Cms.Data;
using Microsoft.AspNetCore.Mvc;
using NewLife.Caching;
using NewLife.Cube.Entity;
using NewLife.Cube.ViewModels;

namespace Leochen.Cms.Controllers;

/// <summary>主页面</summary>
//[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ICacheProvider _cacheProvider;
    private readonly string Arealist = "CmsHome_Arealist";
    private readonly string ContentSortlist = "CmsHome_ContentSortlist_";

    public HomeController(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    /// <summary>主页面</summary>
    /// <returns></returns>
    public ActionResult Index()
    {
        var cache = _cacheProvider.Cache;

        if (!cache.TryGetValue<Dictionary<String, CmsArea>>(Arealist, out var areavalue))
        {
            areavalue = new Dictionary<String, CmsArea>(StringComparer.OrdinalIgnoreCase);
            var list = CmsArea.FindAll();
            foreach (var cmsArea in list)
            {
                areavalue.Add(cmsArea.Domain, cmsArea);
            }
            cache.Set(Arealist, areavalue, 0);
        }
        if (areavalue.Count == 0)
        {
            ViewBag.Message = "未正确设置区域";
            return View("_Error");
        }

        var areaid = 0;
        if (areavalue.Count > 1 && areavalue.TryGetValue(HttpContext.Request.Host.Value, out var cmsarea))
        {
            areaid = cmsarea.ID;
        }
        else
        {
            areaid = areavalue.First().Value.ID;
        }

        if (areaid == 0)
        {
            ViewBag.Message = $"未找到域名{HttpContext.Request.Host.Value},对应的区域";
            return View("_Error");
        }

        if (!cache.TryGetValue<Dictionary<String, CmsContent_Sort>>(ContentSortlist + areaid, out var contentSortlistValue))
        {
            contentSortlistValue = new Dictionary<String, CmsContent_Sort>(StringComparer.OrdinalIgnoreCase);
            var list = CmsContent_Sort.FindAllByAreaID(areaid);
            foreach (var contentSort in list)
            {
                contentSortlistValue.Add(contentSort.Def1, contentSort);
            }
            cache.Set(ContentSortlist + areaid, contentSortlistValue, 0);
        }
        var path = HttpContext.Items["Path"].ToString();
        ViewBag.Message = "主页面";
        ViewBag.path = HttpContext.Items["Path"].ToString();
        return View("_Index");
    }
}