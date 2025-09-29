using Microsoft.AspNetCore.Mvc;
using NewLife.Cube.ViewModels;

namespace Leochen.Cms.Controllers;

/// <summary>主页面</summary>
//[AllowAnonymous]
public class HomeController : Controller
{
    /// <summary>主页面</summary>
    /// <returns></returns>
    public ActionResult Index()
    {
        ViewBag.Message = "主页面";

        return View();
    }
}