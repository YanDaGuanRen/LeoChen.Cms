using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using NewLife.Cube.Services;
using NewLife.Log;

namespace NewLife.Cube.Extensions
{
    public static class DefaultUIServiceCollectionExtensions
    {
        ///// <summary>添加魔方UI</summary>
        ///// <param name="services"></param>
        //public static void AddCubeDefaultUI(this IServiceCollection services) => services.ConfigureOptions(typeof(DefaultUIConfigureOptions));

        /// <summary>使用魔方UI</summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCubeDefaultUI(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 强行设置WebRootPath，避免魔方首次启动下载资源文件后无法马上使用的问题
            //var root = CubeSetting.Current.WebRootPath.GetFullPath();
            var webRoot = CubeSetting.Current.WebRootPath;

            // 优先查找程序集目录，可能是星尘发布的影子目录。然后再找GetFullPath
            var root = AppDomain.CurrentDomain.BaseDirectory.CombinePath(webRoot);
            if (root.IsNullOrEmpty() || !Directory.Exists(root)) root = webRoot.GetFullPath();

            env.WebRootPath = root;

            XTrace.WriteLine("ContentRootPath={0}", env.ContentRootPath);
            XTrace.WriteLine("WebRootPath={0}", env.WebRootPath);

            // 独立静态文件设置，魔方自己的静态资源内嵌在程序集里面
            var options = new StaticFileOptions();
            {
                var embeddedProvider = new CubeEmbeddedFileProvider(Assembly.GetExecutingAssembly(), "NewLife.Cube.wwwroot");
                if (!env.WebRootPath.IsNullOrEmpty() && Directory.Exists(env.WebRootPath))
                    options.FileProvider = new CompositeFileProvider(new PhysicalFileProvider(env.WebRootPath), embeddedProvider);
                else
                    options.FileProvider = embeddedProvider;
            }
            app.UseStaticFiles(options);
            
            return app;
        }
    }
}