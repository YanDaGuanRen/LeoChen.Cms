using System.Text.RegularExpressions;
using NewLife.Log;
using XCode.Membership;

namespace NewLife.Cube;

/// <summary>
/// 文件上传帮助类
/// </summary>
public static class FileUploadHelper
{

    /// <summary>
    /// 保存文件到指定目录
    /// </summary>
    /// <param name="file">上传的文件</param>
    /// <param name="category">文件分类（用于构建目录结构）</param>
    /// <param name="savefileName">保存的文件名不带后缀</param>
    /// <returns>文件的相对路径URL</returns>
    public static async Task<string> SaveFile(IFormFile file, string category="File", string savefileName = null)
    {
        var cubeset = CubeSetting.Current;
        if (category == "Avatar")
        {
            return await SaveFile(file,category,"","",savefileName);
        }
        return await SaveFile(file,category,cubeset.PathFormat,cubeset.SaveFileFormat.ToLower(),savefileName);
    }
    

    /// <summary>
    /// 保存文件到指定目录
    /// </summary>
    /// <param name="file">上传的文件</param>
    /// <param name="category">文件分类（用于构建目录结构）</param>
    /// <param name="pathFormat">文件保存路径格式</param>
    /// <param name="fileNameFormat">文件保存名称格式</param>
    /// <param name="savefileName">保存的文件名不带后缀</param>
    /// <returns>文件的相对路径URL</returns>
    public static async Task<string> SaveFile(IFormFile file, string category, string pathFormat, string fileNameFormat, string savefileName = null)
    {
        try
        {
            if (file == null) return null;
            var extension = Path.GetExtension(file.FileName);
            var (uploadPath, relativeUrl) =
                GeneratePaths(category, pathFormat, fileNameFormat, savefileName, extension);
            uploadPath.EnsureDirectory();
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return relativeUrl;
        }
        catch (Exception e)
        {
            XTrace.WriteException(e);
            return null;
        }
        
    }

    /// <summary>
    /// 保存文件到指定目录
    /// </summary>
    /// <param name="imageBytes">图片字节数组</param>
    /// <param name="category">文件分类（用于构建目录结构）</param>
    /// <param name="pathFormat">文件保存路径格式</param>
    /// <param name="fileNameFormat">文件保存名称格式</param>
    /// <param name="extension">文件扩展名</param>
    /// <returns>文件的相对路径URL</returns>
    public static async Task<string> SaveFile(byte[] imageBytes, string category, string pathFormat, string fileNameFormat, string extension)
    {
        try
        {
            if (imageBytes == null || imageBytes.Length <= 0) return null;
            extension = extension.EnsureStart(".");
            var (uploadPath, relativeUrl) = GeneratePaths(category, pathFormat, fileNameFormat, null, extension);
            uploadPath.EnsureDirectory();
            await System.IO.File.WriteAllBytesAsync(uploadPath, imageBytes);
            return relativeUrl;
        }
        catch (Exception e)
        {
            XTrace.WriteException(e);
            return null;
        }

    }
    private static (string uploadPath, string relativeUrl) GeneratePaths(string category, string pathFormat, string fileNameFormat, string savefileName, string extension)
    {
        if (!category.IsNullOrEmpty())
        {
            category = category.EnsureStart("/");
        }
        var other = "";
        if (!pathFormat.IsNullOrEmpty())
        {
            other = GetSavePath(pathFormat).EnsureStart("/");
        }
        var fileName = "";
        if (!savefileName.IsNullOrEmpty())
        {
            fileName = savefileName + extension;
        }
        else if (!fileNameFormat.IsNullOrEmpty())
        {
            fileName = GetSavePath(fileNameFormat, extension);
        }
        else
        {
            fileName = $"{Guid.NewGuid():N}{extension}";
        }
        fileName = fileName.EnsureStart("/");
        var cubeset = CubeSetting.Current;
        var tenantPath = TenantContext.CurrentId != 0 ? "/" + TenantContext.CurrentId : "";
        var basePath = $"{cubeset.WebRootPath.TrimStart("/").TrimEnd("/")}{tenantPath}{cubeset.UploadPath.EnsureStart("/")}";
        var uploadPath = $"{basePath}{category}{other}{fileName}".GetFullPath();
        var relativeUrl = $"{tenantPath}{cubeset.UploadPath.EnsureStart("/")}{category}{other}{fileName}";
        return (uploadPath, relativeUrl);
    }
    
    /// <summary>
    /// 获取保存路径
    /// </summary>
    /// <param name="pathFormat">路径格式</param>
    /// <param name="extension">文件扩展名 为空不添加后缀中处理路径</param>
    /// <returns></returns>
    public static string GetSavePath(string pathFormat, string extension=null)
    {
        var now = DateTime.Now;
        pathFormat = pathFormat
            .Replace("{yyyy}", now.Year.ToString())
            .Replace("{yy}", now.ToString("yy"))
            .Replace("{mm}", now.Month.ToString("D2"))
            .Replace("{dd}", now.Day.ToString("D2"))
            .Replace("{hh}", now.Hour.ToString("D2"))
            .Replace("{ii}", now.Minute.ToString("D2"))
            .Replace("{ss}", now.Second.ToString("D2"))
            .Replace("{time}", DateTimeOffset.Now.ToUnixTimeSeconds().ToString());
        var rand = new Random();
        pathFormat = Regex.Replace(
            pathFormat, 
            @"\{rand:(\d+)\}", 
            match => {
                var length = int.Parse(match.Groups[1].Value);
                var result = "";
                for (int i = 0; i < length; i++)
                {
                    result += rand.Next(0, 10);
                }
                return result;
                
            });
        if (!extension.IsNullOrEmpty())
        {
            if (!pathFormat.EndsWith(extension, StringComparison.OrdinalIgnoreCase)) pathFormat += extension.EnsureStart(".");
        }
        return Regex.Replace(pathFormat, @"[\|\?""<>\*\+\\\[\]]+", "").EnsureStart("/");
    }
}