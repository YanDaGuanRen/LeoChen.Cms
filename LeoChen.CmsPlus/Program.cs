using LenChen.Cms;
using LenChen.Cms.Services;
using NewLife.Log;
using XCode;

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
    // 把数据目录指向上层，例如部署到 /root/iot/edge/，这些目录放在 /root/iot/
    var set = NewLife.Setting.Current;
    if (set.IsNew)
    {
        set.LogPath = "LogWeb";
        set.DataPath = "Data";
        set.BackupPath = "Backup";
        set.Save();
    }

    var set2 = CubeSetting.Current;
    if (set2.IsNew)
    {
        set2.DateSplitFormat = "yyyyMMdd";
        set2.UploadPath = "Uploads";
        set2.Save();
    }

    var set3 = XCodeSetting.Current;
    if (set3.IsNew)
    {
        // 关闭SQL日志输出
        set3.ShowSQL = false;
        //set3.EntityCacheExpire = 60;
        //set3.SingleCacheExpire = 60;
        set3.Save();
    }
}