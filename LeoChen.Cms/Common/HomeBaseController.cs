using LeoChen.Cms.Data;
using LeoChen.Cms.TemplateEngine;
using Microsoft.AspNetCore.Mvc;
using NewLife.Cube.Entity;

namespace LeoChen.Cms.Common;

public class HomeBaseController:Controller
{
    protected readonly TemplateEngineCache _templateEngineCache;

    protected readonly ITemplateEngine _iTemplateEngine;

    protected CmsArea cmsArea = null;
    protected CmsSite cmsSite = null;
    protected CmsCompany cmsCompany = null;
    protected CmsContent_Sort cmsContentSort = null;
    protected CmsContent cmsContent = null;

    public HomeBaseController(TemplateEngineCache templateEngineCache, ITemplateEngine iTemplateEngine)
    {
        _templateEngineCache = templateEngineCache;
        _iTemplateEngine = iTemplateEngine;
        // _templateEngine = templateEngine;
    }
    
    public ActionResult ParseTemplate(string tplfile,string url)
    {
        return Content(
            _iTemplateEngine.ParseTemplate(tplfile, url, cmsArea,cmsSite, cmsCompany, cmsContent),
            "text/html;charset=utf-8");
    }
    public ActionResult Error()
    {
        var areaid = HttpContext.Items["AreaID"].ToInt(0);
        var message = HttpContext.Items["Message"].ToString();
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
    
    public ActionResult Error(int areaid, string message)
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