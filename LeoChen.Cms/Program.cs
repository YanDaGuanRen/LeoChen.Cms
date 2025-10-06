using LenChen.Cms;
using LenChen.Cms.Services;
using NewLife;
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
var set = WebSetting.Current;
services.AddSingleton(set);

// 启用接口响应压缩
services.AddResponseCompression();

services.AddControllersWithViews();

// 引入魔方
services.AddCube();

// 后台服务
services.AddHostedService<MyHostedService>();
// 先预热数据，再启动Web服务，避免网络连接冲击
services.AddHostedService<PreheatHostedService>();

var app = builder.Build();

// 使用Cube前添加自己的管道
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/CubeHome/Error");

if (Environment.GetEnvironmentVariable("__ASPNETCORE_BROWSER_TOOLS") is null)
    app.UseResponseCompression();


// 使用魔方
app.UseCube(app.Environment);

if (Menu.FindCount() > 0)
{
   var aaa = Menu.FindByName("GlobalConfiguration");
   aaa.Sort = 900;
   aaa.Update();
   aaa = Menu.FindByName("BasicContent");
   aaa.Sort = 800;
   aaa.Update();
   aaa = Menu.FindByName("ArticleContent");
   aaa.Sort = 700;
   aaa.Update();
   aaa = Menu.FindByName("ExpandContent");
   aaa.Sort = 600;
   aaa.Update();
}

app.UseAuthorization();

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
}