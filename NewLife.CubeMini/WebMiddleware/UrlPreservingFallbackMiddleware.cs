using NewLife.Log;
using NewLife.Web;

namespace NewLife.Cube.Middleware;

public class UrlPreservingFallbackMiddleware
{
    private readonly RequestDelegate _next;

    public UrlPreservingFallbackMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
    {
        var path = context.Request.Path.Value?.EnsureStart("/");
        context.Response.StatusCode = 200;
        context.Items["Path"] = path;
        // 重写请求路径
        context.Request.RouteValues["controller"] = "Home";
        context.Request.RouteValues["action"] = "Index";
        context.Request.Path = "/Home/Index";
        await _next(context);
    }
}