using Microsoft.AspNetCore.Http;
using NewLife.Common;
using NewLife.Cube.Common;
using NewLife.Web;
using HttpContext = Microsoft.AspNetCore.Http.HttpContext;
namespace NewLife.Cube.Extensions;

/// <summary>区域相关的HttpContext扩展</summary>
public static class CmsAreaHttpContextExtensions
{
    #region 区域Cookie
    /// <summary>保存选中的区域</summary>
    /// <param name="context"></param>
    /// <param name="areaId"></param>
    public static void SaveArea(this HttpContext context, Int32 areaId)
    {
        var res = context?.Response;
        if (res == null) return;

        var option = new CookieOptions
        {
            SameSite = SameSiteMode.Unspecified,
        };
            
        // https时，SameSite使用None，此时可以让cookie写入有最好的兼容性，跨域也可以读取
        if (context.Request.GetRawUrl().Scheme.EqualIgnoreCase("https"))
        {
            option.SameSite = SameSiteMode.None;
            option.Secure = true;
        }

        if (areaId < 0) option.Expires = DateTimeOffset.MinValue;

        var key = $"AreaId-{SysConfig.Current.Name}";
        res.Cookies.Append(key, areaId + "", option);

        context.SetArea(areaId);
    }

    /// <summary>获取cookie中areaId</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Int32 GetAreaId(this HttpContext context)
    {
        var key = $"AreaId-{SysConfig.Current.Name}";
        var req = context?.Request;
        if (req == null) return -1;

        return req.Cookies[key].ToInt(-1);
    }
        
    /// <summary>设置区域</summary>
    /// <param name="context"></param>
    /// <param name="areaId"></param>
    public static void SetArea(this HttpContext context, Int32 areaId)
    {
        CmsAreaContext.Current = new CmsAreaContext { AreaId = areaId };
    }
    #endregion
}