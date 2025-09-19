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
    /// <param name="isDateSplit">是否按日期分目录</param>
    /// <param name="savefileName">保存的文件名带后缀</param>
    /// <returns>文件的相对路径URL</returns>
    public static async Task<string> SaveFile(IFormFile file, string? category = "File",bool isDateSplit = true,string savefileName="")
    {
        if (file == null) return null;
        // 构建文件保存路径
        var set = CubeSetting.Current;
        var date = "";
        if (isDateSplit && !set.DateSplitFormat.IsNullOrEmpty())
        {
            date ="/" + DateTime.Now.ToString(set.DateSplitFormat);
        }
        var tpath = TenantContext.CurrentId != 0 ? "/" + TenantContext.CurrentId : "";
       var uploadPath = $"{set.WebRootPath}{tpath}/{set.UploadPath}/{category}{date}".GetFullPath();
        uploadPath.EnsureDirectory(false);
        var fileName =savefileName.IsNullOrEmpty()? $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}":savefileName;
        var savePath = Path.Combine(uploadPath, fileName);
        // 保存文件
        using (var stream = new FileStream(savePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        // 返回相对URL路径
        return $"/{set.UploadPath}{tpath}/{category}{date}/{fileName}";
    }

    /// <summary>
    /// 保存头像文件
    /// </summary>
    /// <param name="file">上传的文件</param>
    /// <param name="savefileName">保存的文件名带后缀</param>
    /// <returns>头像的相对路径URL</returns>
    public static async Task<string> SaveAvatar(IFormFile file, string savefileName)
    {
        return await SaveFile(file, "Avatar", false, savefileName);

    }
}