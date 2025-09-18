using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NewLife.Cube.Services;
using NewLife.Cube.ViewModels;

namespace NewLife.Cube.Areas.Admin.Controllers;

/// <summary>系统设置控制器</summary>
[DisplayName("魔方设置")]
[AdminArea]
[Menu(30, true, Icon = "fa-wrench")]
public class CubeController : ConfigController<CubeSetting>
{
    private Boolean _has;

    /// <summary>执行前</summary>
    /// <param name="filterContext"></param>
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (!_has)
        {
            var acelist = new List<string>() { "Ace" };
            var list = GetMembers(typeof(CubeSetting));
            var df = list.FirstOrDefault(e => e.Name == "Theme");
            if (df != null)
            {
                df.Description = $"可选主题 {acelist.Join("/")}";
                df.DataSource = e => acelist.ToDictionary(e => e, e => e);
            }

            df = list.FirstOrDefault(e => e.Name == "Skin");
            if (df != null)
            {
                df.Description = $"可选皮肤 {acelist.Join("/")}";
                df.DataSource = e => acelist.ToDictionary(e => e, e => e);
            }

            df = list.FirstOrDefault(e => e.Name == "EChartsTheme");
            if (df != null)
            {
                var themes = new List<String>() ;
                themes.Insert(0, "default");
                df.Description = $"可选主题 {themes.Join("/")}";
                df.DataSource = e => themes.ToDictionary(e => e, e => e);
            }

            _has = true;
        }

        base.OnActionExecuting(filterContext);

        PageSetting.NavView = "_Object_Nav";
    }

    /// <summary>
    /// 获取登录设置
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public ActionResult GetLoginConfig() => Ok(data: new LoginConfigModel());

    /// <summary>更新时触发</summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override ActionResult Update(CubeSetting obj)
    {
        var rs = base.Update(obj);

        WebHelper2.FixTenantMenu();

        return rs;
    }
}