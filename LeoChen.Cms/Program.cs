using LenChen.Cms;
using LenChen.Cms.Services;
using LeoChen.Cms;
using LeoChen.Cms.Middleware;
using LeoChen.Cms.TemplateEngine;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using NewLife;
using NewLife.Cube.WebMiddleware;
using NewLife.Log;
using XCode;
using XCode.Membership;

// 启用控制台日志，拦截所有异常
XTrace.UseConsole();

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// 初始化配置文件
InitConfig();

// 配置星尘。借助StarAgent，或者读取配置文件 config/star.config 中的服务器地址
var star = services.AddStardust(null);

services.AddRedis();

// 注入应用配置
var cmsset = CmsSetting.Current;
services.AddSingleton(cmsset);

// 启用接口响应压缩
services.AddResponseCompression();

services.AddControllersWithViews();

// 引入魔方
services.AddCube();

// 后台服务
services.AddHostedService<MyHostedService>();
// 先预热数据，再启动Web服务，避免网络连接冲击
services.AddHostedService<PreheatHostedService>();

services.AddSingleton<ITemplateEngine, PbootTemplateEngine>();
services.AddSingleton<TemplateEngineCache>();

var app = builder.Build();

// 使用Cube前添加自己的管道
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/CubeHome/Error");

if (Environment.GetEnvironmentVariable("__ASPNETCORE_BROWSER_TOOLS") is null)
    app.UseResponseCompression();

var webroot = CubeSetting.Current.WebRootPath.GetFullPath();
var provider2 = new FileExtensionContentTypeProvider();
provider2.Mappings.Remove(".html");
provider2.Mappings.Remove(".htm");

//前端路由
app.UseWhen(
    context =>
    {
        var path = context.Request.Path.Value?.ToLowerInvariant();
        var bo = !CubeService.IsAdminPath(path) && !CubeService.IsCubePath(path);
        return bo;
    },
    appBuilder =>
    {
        // 先处理静态文件
        appBuilder.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(webroot),
                RequestPath = "",
                ContentTypeProvider = provider2,
                OnPrepareResponse = ctx =>ctx.Context.Items["FileNotFound"] = !File.Exists(ctx.File.PhysicalPath)
            });
        appBuilder.Use(async (context, next) =>
        {
            XTrace.WriteLine("[CustomMiddleware] 请求路径: {0}", context.Request.Path);
            // 检查是否有文件未找到的标记
            if (context.Items.ContainsKey("FileNotFound") && (bool)context.Items["FileNotFound"])
            {
                XTrace.WriteLine("[CustomMiddleware] 检测到文件未找到标记，返回404");
                context.Response.StatusCode = 404;
                return; // 直接返回，不调用next()
            }

            var path = context.Request.Path.Value;
            if (!string.IsNullOrEmpty(path))
            {
                var extension = Path.GetExtension(path)?.ToLowerInvariant();
                if (!string.IsNullOrEmpty(extension) && provider2.Mappings.ContainsKey(extension))
                {
                    // 这是一个静态资源请求
                    XTrace.WriteLine("[CustomMiddleware] 静态资源请求: {0}", path);
                    // 如果走到这里说明文件不存在（因为如果存在会被StaticFiles处理）
                    context.Response.StatusCode = 404;
                    XTrace.WriteLine("[CustomMiddleware] 静态资源不存在，返回404");
                    return;
                }

            }

            XTrace.WriteLine("[CustomMiddleware] 非静态资源请求，继续处理");
            // 非静态资源请求，继续处理
            await next();
        });
        XTrace.WriteLine("[Program] 注册UrlPreservingFallbackMiddleware");
        appBuilder.UseMiddleware<UrlMiddleware>();
        // 添加这部分代码确保 MVC 路由正常工作
        appBuilder.UseRouting();
        appBuilder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    });
// 使用魔方
app.UseWhen(
    context =>
    {
        var path = context.Request.Path.Value?.ToLower();
        var bo = CubeService.IsAdminPath(path) || CubeService.IsCubePath(path);
        return bo; 
    },
    appBuilder =>
    {
        appBuilder.UseCube(app.Environment);
        appBuilder.UseAuthorization();
        appBuilder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    });

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}"
);

// 注册退出事件
if (app is IHost host)
    NewLife.Model.Host.RegisterExit(() => host.StopAsync().Wait());

app.Run();


static void InitConfig()
{
    "wwwroot".GetFullPath().EnsureDirectory(false);
    // 把数据目录指向根目录，可以使用../指向上一级目录
    var set = NewLife.Setting.Current;
    if (set.IsNew)
    {
        set.LogPath = "LogWeb";
        set.DataPath = "Data";
        set.BackupPath = "Backup";
        set.Save();
    }

    set.LogPath.EnsureDirectory(false);
    set.DataPath.EnsureDirectory(false);
    set.BackupPath.EnsureDirectory(false);

    var set2 = CubeSetting.Current;
    if (set2.IsNew)
    {
        set2.UploadPath = "Uploads";
        set2.Save();
    }

    set2.UploadPath.EnsureDirectory(false);

    var set3 = XCodeSetting.Current;
    if (set3.IsNew)
    {
        // 关闭SQL日志输出
        set3.ShowSQL = false;
        set3.EntityCacheExpire = 60;
        set3.SingleCacheExpire = 60;
        set3.Save();
    }

    "RunTime".EnsureDirectory(false);
    "RunTime/Template".EnsureDirectory(false);
    "Template".EnsureDirectory(false);
}