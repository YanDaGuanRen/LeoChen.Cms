using LeoChen.Cms.TemplateEngine;
using NewLife.Cube.Entity;
using NewLife.Cube.Services;
using NewLife.Cube.Web;
using NewLife.Log;
using NewLife.Web;

namespace LeoChen.Cms.Middleware;

public class UrlMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AccessService _accessService;
    private readonly TemplateEngineCache _templateEngineCache;

    public UrlMiddleware(RequestDelegate next,AccessService accessService,TemplateEngineCache templateEngineCache)
    {
        _next = next;
        _accessService = accessService;
        _templateEngineCache = templateEngineCache;
    }
    

    public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
    {
        context.Items["UserAgent"] = context.Request.Headers.GetUserAgent();;
        var url = context.Request.GetRawUrl();
        context.Items["CmsUrl"] = url;
        // var cmsset = CmsSetting.Current;
        // // var rule = _accessService.Valid(url + "", ua, ip, user, session);
        // // if (rule != null && rule.ActionKind is AccessActionKinds.Block or AccessActionKinds.Limit)
        // // {
        // //     if (rule.BlockCode == 302)
        // //     {
        // //         context.Response.Redirect(rule.BlockContent);
        // //     }
        // //     else if (rule.BlockCode > 0)
        // //     {
        // //         context.Response.StatusCode = rule.BlockCode;
        // //         context.Response.ContentType = "text/html; charset=utf-8";
        // //         await context.Response.WriteAsync(rule.BlockContent);
        // //         await context.Response.CompleteAsync();
        // //     }
        // //     else
        // //     {
        // //         context.Abort();
        // //     }
        // //
        // //     return;
        // // }
        // context.Response.StatusCode = 200;
        //
        // if (url.LocalPath.IsNullOrEmpty() || url.LocalPath == "/index.html")
        // {
        //     context.Response.Redirect($"{context.Request.Scheme}://{context.Request.Host}/", permanent: true);
        //     return;
        // }
        //
        // if (!_templateEngineCache.GetCacheAreaList(out var arealist))
        // {
        //
        //     await ExecuteInvokeAsync(context, "Home", "Error");
        //     return;
        // }
        //
        // //如果没取到就用第一个
        // if (!arealist.TryGetValue(host, out var cmsArea))
        // {
        //     cmsArea = arealist.First().Value;
        // }
        //
        // if (!_templateEngineCache.GetCacheSite(cmsArea.ID, out var cmsSite))
        // {
        //     await ExecuteInvokeAsync(context, "Home", "Error");
        //     return;
        // }
        
        // var cmsp = CmsSetting.Provider as CmsDBConfigProvider;
        // cmsp.SetValueForTenant("CloseSite","True",0);
        // var cccc = cmsp.GetValueForTenant("closesite", 0);
        // var dddd = cmsp.GetValueForTenant("closesite", 1);
        // dddd = cmsp.GetValueForTenant("LoginNum", 1);
        // cmsp.SetValueForTenant("LoginNum","3",0);
        // dddd = cmsp.GetValueForTenant("LoginNum", 1);
        // cmsp.SetValueForTenant("CloseSite","False",0);
        // dddd = cmsp.GetValueForTenant("closesite", 1);
        //
        // cmsp.SetValueForTenant("CloseSite","True",0);
        // dddd = cmsp.GetValueForTenant("closesite", 1);
        // cmsp.SetValueForTenant("CloseSite","False",0);
        // dddd = cmsp.GetValueForTenant("closesite", 0);
        // if (cmsset.CloseSite)
        // {
        //     ViewBag.Message = cmsset.CloseSiteNote;
        //     ViewBag.Title = "网站关闭";
        //     return View("_Error");
        // }
        //
        // if (baseurl.LocalPath.IsNullOrEmpty() || baseurl.LocalPath == "/")
        // {
        //     path = "/index.html";
        // }
        // else
        // {
        //     path = baseurl.LocalPath;
        // }
        // var cacheTemplatePath = $"RunTimes/Template/{cmsArea.TenantId}/{cmsArea.ID}";
        // var cachefile = cacheTemplatePath.CombinePath(path).GetFullPath();
        // if (System.IO.File.Exists(cachefile))
        // {
        //     var html = new StringBuilder(System.IO.File.ReadAllText(cachefile));
        //     return Content(html.ToString(), "text/html;charset=utf-8");
        // }
        //
        // //网站首页
        // if (path == "/index.html")
        // {
        //     return ParseTemplate("index.html","index.html");
        // }
        //
        // //网站地图处理
        // if (path.StartsWith("sitemap"))
        // {
        // }
        //
        // var urlx = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        // var homeSort = urlx[0];
        // var homeAction = urlx[^1];
        //
        // if (!_templateEngineCache.GetContentSortDic(cmsArea.ID, out var cmsContentSortDic))
        // {
        //     return Error(cmsArea.ID, $"未正确区域{cmsArea.Name}中的栏目信息");
        // }
        //
        // foreach (var sort in cmsContentSortDic)
        // {
        //     if (homeSort.EqualIgnoreCase(sort.Value.UrlName) || homeAction.EqualIgnoreCase(sort.Value.Filename))
        //     {
        //         cmsContentSort = sort.Value;
        //         break;
        //     }
        // }
        //
        // if (cmsContentSort == null)
        // {
        //     return Error(cmsArea.ID, $"未找到!!!!!\r\n区域:{cmsArea.Name}\r\n栏目.Url名称:{homeSort}");
        // }
        //
        // if (!homeAction.EndsWith(".html"))
        // {
        //     //利于SEO 
        //     if (cmsContentSort.Model.ModelType == CmsModelType.列表)
        //     {
        //         return RedirectPermanent($"/{homeSort}/list-0.html");
        //     }
        //
        //     if (cmsContentSort.Model.ModelType == CmsModelType.单页 )
        //     {
        //         return RedirectPermanent($"/{homeSort}/index.html");
        //     }
        // }
        // cmsContent = CmsContent.FindByID(0);
        // return ParseTemplate(cmsContentSort.ContentTpl,homeAction);
        //
        //
        
        
        
        await ExecuteInvokeAsync(context,"Home","Index");
    }
    
    /// <summary>
    /// 默认执行
    /// </summary>
    /// <param name="context"></param>
    /// <param name="controller"></param>
    /// <param name="action"></param>
    private async Task ExecuteInvokeAsync(Microsoft.AspNetCore.Http.HttpContext context,string controller , string action)
    {
        context.Request.RouteValues["controller"] = controller;
        context.Request.RouteValues["action"] = action;
        context.Request.Path = $"/{controller}/{action}";
        await _next(context);
    }
}