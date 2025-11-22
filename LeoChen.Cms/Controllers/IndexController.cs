using System.Text;
using LeoChen.Cms;
using LeoChen.Cms.Common;
using LeoChen.Cms.Data;
using LeoChen.Cms.TemplateEngine;
using Microsoft.AspNetCore.Mvc;
using NewLife.Caching;
using NewLife.Cube.Configuration;
using NewLife.Cube.Entity;
using NewLife.Cube.ViewModels;
using NewLife.Cube.Web;

namespace Leochen.Cms.Controllers;

/// <summary>主页面</summary>
//[AllowAnonymous]
public class IndexController : HomeBaseController
{
    public IndexController(TemplateEngineCache templateEngineCache, ITemplateEngine iTemplateEngine) : base(templateEngineCache, iTemplateEngine)
    {
    }

    /// <summary>主页面</summary>
    /// <returns></returns>
    public ActionResult Index()
    {
        var cmsSet = CmsSetting.Current;
        var baseurl = HttpContext.Items["CmsUrl"]as Uri;
        var ua = HttpContext.Items["UserAgent"]as UserAgentParser;
        var host = HttpContext.Request.Host.Value;
        string path;
        if (!_templateEngineCache.GetCacheAreaList(out var arealist))
        {
            return Error(0, $"未正确区域");
        }

        //如果没取到就用第一个
        if (!arealist.TryGetValue(host, out cmsArea))
        {
            cmsArea = arealist.First().Value;
        }

        if (!_templateEngineCache.GetCacheSite(cmsArea.ID, out cmsSite))
        {
            return Error(cmsArea.ID, $"未正确区域{cmsArea.Name}中的站点信息");
        }

        if (cmsSet.CloseSite)
        {
            ViewBag.Message = cmsSet.CloseSiteNote;
            ViewBag.Title = "网站关闭";
            return View("_Error");
        }

        if (baseurl.LocalPath.IsNullOrEmpty() || baseurl.LocalPath == "/")
        {
            path = "/index.html";
        }
        else
        {
            path = baseurl.LocalPath;
        }
        var cacheTemplatePath = $"RunTimes/Template/{cmsArea.TenantId}/{cmsArea.ID}";
        var cachefile = cacheTemplatePath.CombinePath(path).GetFullPath();
        if (System.IO.File.Exists(cachefile))
        {
            var html = new StringBuilder(System.IO.File.ReadAllText(cachefile));
            return Content(html.ToString(), "text/html;charset=utf-8");
        }

        //网站首页
        if (path == "/index.html")
        {
            return ParseTemplate("index.html","index.html");
        }

        //网站地图处理
        if (path.StartsWith("sitemap"))
        {
        }

        var urlx = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var homeSort = urlx[0];
        var homeAction = urlx[^1];
        
        if (!_templateEngineCache.GetContentSortDic(cmsArea.ID, out var cmsContentSortDic))
        {
            return Error(cmsArea.ID, $"未正确区域{cmsArea.Name}中的栏目信息");
        }

        foreach (var sort in cmsContentSortDic)
        {
            if (homeSort.EqualIgnoreCase(sort.Value.UrlName) || homeAction.EqualIgnoreCase(sort.Value.Filename))
            {
                cmsContentSort = sort.Value;
                break;
            }
        }

        if (cmsContentSort == null)
        {
            return Error(cmsArea.ID, $"未找到!!!!!\r\n区域:{cmsArea.Name}\r\n栏目.Url名称:{homeSort}");
        }

        if (!homeAction.EndsWith(".html"))
        {
            //利于SEO 
            if (cmsContentSort.Model.ModelType == CmsModelType.列表)
            {
                return RedirectPermanent($"/{homeSort}/list-0.html");
            }

            if (cmsContentSort.Model.ModelType == CmsModelType.单页 )
            {
                return RedirectPermanent($"/{homeSort}/index.html");
            }
        }
        cmsContent = CmsContent.FindByID(0);
        return ParseTemplate(cmsContentSort.ContentTpl,homeAction);

    }

    public ActionResult ParseTemplate(string tplfile,string url)
    {
        return Content(
            _iTemplateEngine.ParseTemplate(tplfile, url, cmsArea,cmsSite, cmsCompany, cmsContent),
            "text/html;charset=utf-8");
    }

    public ActionResult Error(int areaid = 0, string? message = "未知错误")
    {

        if (areaid < 1)
        {
            ViewBag.Message = message;
            return View("_Error");
        }

        if (!_templateEngineCache.GetCaCheTemplatePath(areaid, out var path) || !System.IO.File.Exists(path + "404.html"))
        {
            ViewBag.Message = message;
            return View("_Error");
        }

        return ParseTemplate("404.html","404.html");
    }
}