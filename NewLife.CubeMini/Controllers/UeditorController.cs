using System.Buffers;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NewLife.Cube.Areas.Cube.Controllers;
using NewLife.Cube.Entity;
using NewLife.Cube.Extensions;
using NewLife.Cube.Models;
using NewLife.Cube.Services;
using NewLife.Data;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Web;
using XCode;
using XCode.Membership;
using static XCode.Membership.User;
using AreaX = XCode.Membership.Area;
using HttpContext = Microsoft.AspNetCore.Http.HttpContext;


namespace NewLife.Cube.Controllers;

/// <summary>UEditor接口</summary>
[DisplayName("UEditor接口")]
public class UeditorController(TokenService tokenService, IEnumerable<EndpointDataSource> sources) : ControllerBaseX
{
    #region 拦截

    /// <summary>执行前</summary>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // 仅对未标注 [AllowAnonymous] 的接口进行登录校验
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var allowAnonymous = descriptor?.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true)
            .FirstOrDefault();
        if (allowAnonymous == null && !ValidateToken())
        {
            var req = context.HttpContext.Request;
            var accept = (req.Headers["Accept"] + "").ToLowerInvariant();

            // 按客户端期望返回：
            // 1) 接受 json -> 返回 Json 封装的401
            if (accept.Contains("json"))
            {
                context.Result = Json(401, "未授权");
                return;
            }

            // 2) 接受二进制下载或HTML -> 返回HTTP 401状态码
            if (accept.Contains("octet-stream"))
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            // 3) 其它情况 -> 跳转登录页并携带returl
            var retUrl = req.GetEncodedPathAndQuery();
            var rurl = "~/Admin/User/Login".AppendReturn(retUrl);
            context.Result = new RedirectResult(rurl);
            return;
        }

        base.OnActionExecuting(context);
    }

    /// <summary>执行后</summary>
    /// <param name="context"></param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null && !context.ExceptionHandled)
        {
            var ex = context.Exception.GetTrue();
            context.Result = Json(0, null, ex);
            context.ExceptionHandled = true;

            if (XTrace.Debug) XTrace.WriteException(ex);

            return;
        }

        base.OnActionExecuted(context);
    }

    private Boolean ValidateToken()
    {
        var logined = ManageProvider.User != null;
        if (logined) return true;

        var token = GetToken(HttpContext);
        if (!token.IsNullOrEmpty())
        {
            var ap = tokenService.FindBySecret(token);
            if (ap != null && ap.Enable)
                logined = true;
            else
            {
                var set = CubeSetting.Current;
                var (app, ex) = tokenService.TryDecodeToken(token, set.JwtSecret);
                if (app != null && app.Enable && ex != null) logined = true;
            }
        }

        return logined;
    }

    /// <summary>从请求头中获取令牌</summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static String GetToken(HttpContext httpContext)
    {
        var request = httpContext.Request;
        var token = request.Query["Token"] + "";
        if (token.IsNullOrEmpty()) token = (request.Headers["Authorization"] + "").TrimStart("Bearer ");
        if (token.IsNullOrEmpty()) token = request.Headers["X-Token"] + "";
        if (token.IsNullOrEmpty()) token = request.Cookies["Token"] + "";

        return token;
    }

    #endregion

    [AllowAnonymous]
    public ActionResult Index()
    {
        return Json(0, "ok");
    }

    private JsonResult Config()
    {
        var cubeset = CubeSetting.Current;
        var ueset = UEditorSetting.Current;
        var tenant = (cubeset.EnableTenant && TenantContext.CurrentId != 0) ? "/" + TenantContext.CurrentId : "";
        ;
        var urlPrefix = $"{tenant}/{cubeset.UploadPath.EnsureEnd("/")}";
        // 构建UEditor需要的配置对象
        var config = new Dictionary<string, object>
        {
            // 上传图片配置
            ["imageActionName"] = "uploadimage",
            ["imageFieldName"] = "upfile",
            ["imageMaxSize"] = ueset.ImageMaxSize,
            ["imageAllowFiles"] = ueset.ImageAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
            ["imageCompressEnable"] = ueset.ImageCompressEnable,
            ["imageCompressBorder"] = ueset.ImageCompressBorder,
            ["imageInsertAlign"] = ueset.ImageInsertAlign,
            ["imageUrlPrefix"] = "",
            ["imagePathFormat"] = ueset.ImagePathFormat.TrimStart("/"),

            // 涂鸦配置
            ["scrawlActionName"] = "uploadscrawl",
            ["scrawlFieldName"] = "upfile",
            ["scrawlMaxSize"] = ueset.ScrawlMaxSize,
            ["scrawlInsertAlign"] = ueset.ScrawlInsertAlign,
            ["scrawlUrlPrefix"] = "",
            ["scrawlPathFormat"] = ueset.ScrawlPathFormat.TrimStart("/"),

            // 截图配置
            ["snapscreenActionName"] = "uploadimage",
            ["snapscreenInsertAlign"] = ueset.SnapscreenInsertAlign,
            ["snapscreenUrlPrefix"] = "",
            ["snapscreenPathFormat"] = ueset.SnapscreenPathFormat.TrimStart("/"),

            // 抓取远程图片配置
            ["catcherLocalDomain"] = ueset.CatcherLocalDomain.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
            ["catcherActionName"] = "catchimage",
            ["catcherFieldName"] = "source",
            ["catcherMaxSize"] = ueset.CatcherMaxSize,
            ["catcherAllowFiles"] = ueset.CatcherAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
            ["catcherUrlPrefix"] = "",
            ["catcherPathFormat"] = ueset.CatcherPathFormat.TrimStart("/"),

            // 上传视频配置
            ["videoActionName"] = "uploadvideo",
            ["videoFieldName"] = "upfile",
            ["videoAllowFiles"] = ueset.VideoAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
            ["videoMaxSize"] = ueset.VideoMaxSize,
            ["videoUrlPrefix"] = "",
            ["videoPathFormat"] = ueset.VideoPathFormat.TrimStart("/"),

            // 上传文件配置
            ["fileActionName"] = "uploadfile",
            ["fileFieldName"] = "upfile",
            ["fileMaxSize"] = ueset.FileMaxSize,
            ["fileAllowFiles"] = ueset.FileAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
            ["fileUrlPrefix"] = "",
            ["filePathFormat"] = ueset.FilePathFormat.TrimStart("/"),

            // 列出图片配置
            ["imageManagerActionName"] = "listimage",
            ["imageManagerListPath"] = urlPrefix + "image/",
            ["imageManagerListSize"] = ueset.ImageManagerListSize,
            ["imageManagerInsertAlign"] = ueset.ImageManagerInsertAlign,
            ["imageManagerAllowFiles"] = ueset.ImageManagerAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
            ["imageManagerUrlPrefix"] = "",

            // 列出文件配置
            ["fileManagerActionName"] = "listfile",
            ["fileManagerListPath"] = urlPrefix + "file/",
            ["fileManagerListSize"] = ueset.FileManagerListSize,
            ["fileManagerAllowFiles"] = ueset.FileManagerAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
            ["fileManagerUrlPrefix"] = "",
        };

        return Json(config);
    }

    [HttpGet]
    [HttpPost]
    public async Task<IActionResult> Exec()
    {
        var pars = HttpContext.GetParams(false, true, false, false, true);
        if (!pars.TryGetValue("Action", out var actionName))
        {
            return Json(new { state = "请求地址出错" });
        }

        switch (actionName)
        {
            case "config":
                return Config();

            case "uploadimage":
                return await UploadImage();

            case "uploadscrawl":
                return await UploadScrawl();

            case "uploadvideo":
                return await UploadVideo();

            case "uploadfile":
                return await UploadFile();

            case "listimage":
                return ListImage();

            case "listfile":
                return ListFile();

            case "catchimage":
                return await CatchImage();

            default:
                return Json(new { state = "请求地址出错" });
        }
    }

    private async Task<IActionResult> UploadImage()
    {
        var _config = UEditorSetting.Current;
        return await UploadFileBase(
            "image",
            "upfile",
            _config.ImagePathFormat,
            _config.ImageMaxSize,
            _config.ImageAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
        );
    }

    private async Task<IActionResult> UploadScrawl()
    {
        var _config = UEditorSetting.Current;
        var base64Data = Request.Form["upfile"].ToString();
        return await UploadBase64(
            base64Data,
            "image",
            _config.ScrawlPathFormat,
            _config.ScrawlMaxSize,
            new List<string> { ".png", ".jpg", ".jpeg", ".gif", ".bmp" }
        );
    }

    private async Task<IActionResult> UploadVideo()
    {
        var _config = UEditorSetting.Current;
        return await UploadFileBase(
            "video",
            "upfile",
            _config.VideoPathFormat,
            _config.VideoMaxSize,
            _config.VideoAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
        );
    }

    private async Task<IActionResult> UploadFile()
    {
        var _config = UEditorSetting.Current;
        return await UploadFileBase(
            "file",
            "upfile",
            _config.FilePathFormat,
            _config.FileMaxSize,
            _config.FileAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
        );
    }

    private IActionResult ListImage()
    {
        var cubeset = CubeSetting.Current;
        var _config = UEditorSetting.Current;
        var tenant = (cubeset.EnableTenant && TenantContext.CurrentId != 0) ? "/" + TenantContext.CurrentId : "";
        ;
        var urlPrefix = $"{tenant}/{cubeset.UploadPath.EnsureEnd("/")}";
        return ListFileBase(
            urlPrefix + "image/",
            _config.ImageManagerListSize,
            _config.ImageManagerAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
        );
    }

    private IActionResult ListFile()
    {
        var cubeset = CubeSetting.Current;
        var _config = UEditorSetting.Current;
        var tenant = (cubeset.EnableTenant && TenantContext.CurrentId != 0) ? "/" + TenantContext.CurrentId : "";
        var urlPrefix = $"{tenant}/{cubeset.UploadPath.EnsureEnd("/")}";
        return ListFileBase(
            urlPrefix + "file/",
            _config.FileManagerListSize,
            _config.FileManagerAllowFiles.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
        );
    }

    private async Task<IActionResult> CatchImage()
    {
        var _config = UEditorSetting.Current;
        try
        {
            var source = Request.Form["source"].ToArray();
            var list = new List<UEditorCatchItem>();

            using (var httpClient = new HttpClient())
            {
                // 设置超时时间
                httpClient.Timeout = TimeSpan.FromMinutes(5);

                foreach (var imgUrl in source)
                {
                    try
                    {
                        // 验证URL
                        if (!Uri.TryCreate(imgUrl, UriKind.Absolute, out var uri) ||
                            (uri.Scheme != "http" && uri.Scheme != "https"))
                        {
                            list.Add(new UEditorCatchItem { State = "链接不是http链接", Source = imgUrl });
                            continue;
                        }

                        // 检查域名是否在允许列表中
                        if (!_config.CatcherLocalDomain.Contains(uri.Host))
                        {
                            list.Add(new UEditorCatchItem { State = "链接域名不被允许", Source = imgUrl });
                            continue;
                        }

                        // 下载图片
                        var response = await httpClient.GetAsync(imgUrl);
                        if (!response.IsSuccessStatusCode)
                        {
                            list.Add(new UEditorCatchItem { State = "下载失败", Source = imgUrl });
                            continue;
                        }

                        var contentBytes = await response.Content.ReadAsByteArrayAsync();
                        if (contentBytes.Length > _config.CatcherMaxSize)
                        {
                            list.Add(new UEditorCatchItem { State = "文件大小超出限制", Source = imgUrl });
                            continue;
                        }

                        var contentType = response.Content.Headers.ContentType?.MediaType ?? "";
                        if (!contentType.StartsWith("image/"))
                        {
                            list.Add(new UEditorCatchItem { State = "链接contentType不正确", Source = imgUrl });
                            continue;
                        }

                        // 获取文件扩展名
                        var extension = contentType switch
                        {
                            "image/png" => ".png",
                            "image/gif" => ".gif",
                            "image/bmp" => ".bmp",
                            _ => ".jpg"
                        };

                        if (!_config.CatcherAllowFiles.Contains(extension))
                        {
                            list.Add(new UEditorCatchItem { State = "文件类型不允许", Source = imgUrl });
                            continue;
                        }

                        var saveUrl = await FileUploadHelper.SaveFile(
                            contentBytes,
                            "image",
                            _config.CatcherPathFormat,
                            _config.CatcherPathFormat,
                            extension
                        );

                        list.Add(new UEditorCatchItem
                        {
                            State = "SUCCESS",
                            Url = saveUrl,
                            Size = contentBytes.Length,
                            Title = Path.GetFileName(saveUrl),
                            Original = Path.GetFileName(saveUrl),
                            Source = imgUrl
                        });
                    }
                    catch (Exception ex)
                    {
                        list.Add(new UEditorCatchItem { State = "抓取失败: " + ex.Message, Source = imgUrl });
                    }
                }
            }

            var result = new UEditorCatchResult
            {
                State = list.Count > 0 ? "SUCCESS" : "ERROR",
                List = list.ToArray()
            };

            return Json(result);
        }
        catch (Exception ex)
        {
            return Json(new UEditorCatchResult { State = "抓取远程图片失败: " + ex.Message });
        }
    }

    private async Task<IActionResult> UploadFileBase(string category, string fieldName, string pathFormat, int maxSize, List<string> allowFiles)
    {
        var _config = UEditorSetting.Current;
        try
        {
            var file = Request.Form.Files[fieldName];
            if (file == null || file.Length == 0)
            {
                return Json(new UEditorUploadResult { State = "上传文件为空" });
            }

            if (file.Length > maxSize)
            {
                return Json(new UEditorUploadResult { State = "文件大小超出限制" });
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowFiles.Contains(fileExtension))
            {
                return Json(new UEditorUploadResult { State = "文件类型不允许" });
            }

            var fileNameFormat = "";
            if (pathFormat.IsNullOrEmpty())
            {
                pathFormat = "{yyyy}{mm}{dd}/{time}{rand:6}";
            }

            var strFormat = pathFormat.Split("/", StringSplitOptions.RemoveEmptyEntries);
            if (strFormat.Length >= 2)
            {
                pathFormat = strFormat[0];
                fileNameFormat = strFormat[1];
            }
            else
            {
                fileNameFormat = "{time}{rand:6}";
            }

            var saveUrl = await FileUploadHelper.SaveFile(file, category, pathFormat, fileNameFormat);

            var result = new UEditorUploadResult
            {
                State = "SUCCESS",
                Url = saveUrl,
                Title = Path.GetFileName(saveUrl),
                Original = file.FileName,
                Type = fileExtension,
                Size = file.Length
            };

            return Json(result);
        }
        catch (Exception ex)
        {
            return Json(new UEditorUploadResult { State = "上传失败: " + ex.Message });
        }
    }

    private async Task<IActionResult> UploadBase64(string base64Data, string category, string pathFormat, int maxSize,
        List<string> allowFiles)
    {
        var _config = UEditorSetting.Current;
        try
        {
            // 移除base64数据URL前缀
            var base64Regex = new Regex(@"^data:image\/(\w+);base64,");
            var match = base64Regex.Match(base64Data);
            if (!match.Success)
            {
                return Json(new UEditorUploadResult { State = "数据格式错误" });
            }

            var imageFormat = match.Groups[1].Value;
            var extension = "." + imageFormat;

            // 验证文件类型
            if (!allowFiles.Contains(extension))
            {
                return Json(new UEditorUploadResult { State = "文件类型不允许" });
            }

            // 解码base64数据
            var base64 = base64Data.Substring(match.Length);
            var imageBytes = Convert.FromBase64String(base64);

            if (imageBytes.Length > maxSize)
            {
                return Json(new UEditorUploadResult { State = "文件大小超出限制" });
            }

            var fileNameFormat = "";
            if (pathFormat.IsNullOrEmpty())
            {
                pathFormat = "{yyyy}{mm}{dd}/{time}{rand:6}";
            }

            var strFormat = pathFormat.Split("/", StringSplitOptions.RemoveEmptyEntries);
            if (strFormat.Length >= 2)
            {
                pathFormat = strFormat[0];
                fileNameFormat = strFormat[1];
            }
            else
            {
                fileNameFormat = "{time}{rand:6}";
            }

            var saveUrl = await FileUploadHelper.SaveFile(imageBytes, category, pathFormat, fileNameFormat, extension);
            var result = new UEditorUploadResult
            {
                State = "SUCCESS",
                Url = saveUrl,
                Title = Path.GetFileName(saveUrl),
                Original = "TODO",
                Type = extension,
                Size = imageBytes.Length
            };

            return Json(result);
        }
        catch (Exception ex)
        {
            return Json(new UEditorUploadResult { State = "上传失败: " + ex.Message });
        }
    }

    private IActionResult ListFileBase(string listPath, int listSize, List<string> allowFiles)
    {
        try
        {
            var startStr = Request.Query["start"].ToString();
            var sizeStr = Request.Query["size"].ToString();

            var startIndex = string.IsNullOrEmpty(startStr) ? 0 : int.Parse(startStr);
            var size = string.IsNullOrEmpty(sizeStr) ? listSize : int.Parse(sizeStr);
            var cubeset = CubeSetting.Current;
            var fullPath = $"{cubeset.WebRootPath.Trim('/')}{listPath.EnsureStart("/")}";
            fullPath = fullPath.GetFullPath();
            fullPath.EnsureDirectory(false);
            var fileList = new List<UEditorFileItem>();

            if (Directory.Exists(fullPath))
            {
                var files = Directory.GetFiles(fullPath, "*.*", SearchOption.AllDirectories);
                // 按修改时间倒序排列
                files = files.OrderByDescending<string, DateTime>(f => System.IO.File.GetLastWriteTime(f))
                    .ToArray();

                for (int i = startIndex; i < Math.Min(startIndex + size, files.Length); i++)
                {
                    var file = files[i];
                    var extension = Path.GetExtension(file).ToLower();
                    if (allowFiles.Contains(extension))
                    {
                        var relativePath = file.Substring(cubeset.WebRootPath.Trim('/').GetFullPath().Length).Replace(Path.DirectorySeparatorChar+"","/");
                        fileList.Add(new UEditorFileItem
                        {
                            Url = relativePath,
                            Mtime = new DateTimeOffset(System.IO.File.GetLastWriteTime(file)).ToUnixTimeSeconds()
                        });
                    }
                }
            }

            var result = new UEditorListResult
            {
                State = "SUCCESS",
                List = fileList.ToArray(),
                Start = startIndex,
                Total = fileList.Count
            };

            return Json(result);
        }
        catch (Exception ex)
        {
            return Json(new UEditorListResult { State = "获取文件列表失败: " + ex.Message });
        }
    }
}