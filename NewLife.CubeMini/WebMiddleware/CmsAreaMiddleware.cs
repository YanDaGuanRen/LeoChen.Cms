using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using NewLife.Cube.Common;
using NewLife.Cube.Entity;
using NewLife.Cube.Extensions;

namespace NewLife.Cube.WebMiddleware
{
    /// <summary>区域中间件。设置区域上下文</summary>
    public class CmsAreaMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>实例化</summary>
        /// <param name="next"></param>
        public CmsAreaMiddleware(RequestDelegate next) => _next = next;

        /// <summary>调用</summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext ctx)
        {
            // 找到区域，并设置上下文。该上下文将全局影响魔方和XCode
            var changed = false;
            try
            {
                if (CmsAreaContext.Current == null)
                {
                    var areaId = ctx.GetAreaId();
                    if (areaId > 0)
                    {
                        ctx.SetArea(areaId);

                        changed = true;
                    }
                    else
                    {
                        // 如果没有设置区域，使用默认区域
                        var areas = CmsArea.FindAllWithCache();
                        if (areas.Count > 0)
                        {
                            ctx.SaveArea(areas[0].ID);
                            changed = true;
                        }
                    }
                }

                await _next.Invoke(ctx);
            }
            finally
            {
                if (changed)
                {
                    CmsAreaContext.Current = null;
                }
            }
        }
    }
}