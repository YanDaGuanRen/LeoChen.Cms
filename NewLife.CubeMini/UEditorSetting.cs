using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Configuration;

namespace NewLife.Cube
{
    /// <summary>UEditor配置</summary>
    [DisplayName("UEditor配置")]
    [Config("UEditor")]
    public class UEditorSetting : Config<UEditorSetting>
    {
        #region 上传图片配置项

        /// <summary>上传大小限制，单位B</summary>
        [Description("上传大小限制。单位B")]
        [Category("上传图片配置项")]
        public int ImageMaxSize { get; set; } = 20480000;

        /// <summary>上传图片格式显示</summary>
        [Description("上传图片格式显示")]
        [Category("上传图片配置项")]
        public string ImageAllowFiles { get; set; } = ".png,.jpg,.jpeg,.gif,.bmp" ;

        /// <summary>是否压缩图片。默认是true</summary>
        [Description("是否压缩图片。默认是true")]
        [Category("上传图片配置项")]
        public bool ImageCompressEnable { get; set; } = false;

        /// <summary>图片压缩最长边限制</summary>
        [Description("图片压缩最长边限制")]
        [Category("上传图片配置项")]
        public int ImageCompressBorder { get; set; } = 1600;

        /// <summary>插入的图片浮动方式</summary>
        [Description("插入的图片浮动方式")]
        [Category("上传图片配置项")]
        public string ImageInsertAlign { get; set; } = "none";

        /// <summary>上传保存路径,可以自定义保存路径和文件名格式</summary>
        [Description("上传保存路径。可以自定义保存路径和文件名格式")]
        [Category("上传图片配置项")]
        public string ImagePathFormat { get; set; } = "{yyyy}{mm}{dd}/{time}{rand:6}";
        #endregion

        #region 涂鸦图片上传配置项

        /// <summary>上传保存路径,可以自定义保存路径和文件名格式</summary>
        [Description("上传保存路径。可以自定义保存路径和文件名格式")]
        [Category("涂鸦图片上传配置项")]
        public string ScrawlPathFormat { get; set; } = "{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>上传大小限制。单位B</summary>
        [Description("上传大小限制。单位B")]
        [Category("涂鸦图片上传配置项")]
        public int ScrawlMaxSize { get; set; } = 20480000;

        /// <summary>插入的图片浮动方式</summary>
        [Description("插入的图片浮动方式")]
        [Category("涂鸦图片上传配置项")]
        public string ScrawlInsertAlign { get; set; } = "none";
        #endregion

        #region 截图工具上传

        /// <summary>上传保存路径。可以自定义保存路径和文件名格式</summary>
        [Description("上传保存路径。可以自定义保存路径和文件名格式")]
        [Category("截图工具上传")]
        public string SnapscreenPathFormat { get; set; } = "{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>插入的图片浮动方式</summary>
        [Description("插入的图片浮动方式")]
        [Category("截图工具上传")]
        public string SnapscreenInsertAlign { get; set; } = "none";
        #endregion

        #region 抓取远程图片配置
        /// <summary>本地域名</summary>
        [Description("本地域名")]
        [Category("抓取远程图片配置")]
        public string CatcherLocalDomain { get; set; } = "127.0.0.1,localhost,img.baidu.com" ;

        /// <summary>上传保存路径。可以自定义保存路径和文件名格式</summary>
        [Description("上传保存路径。可以自定义保存路径和文件名格式")]
        [Category("抓取远程图片配置")]
        public string CatcherPathFormat { get; set; } = "{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>上传大小限制。单位B</summary>
        [Description("上传大小限制。单位B")]
        [Category("抓取远程图片配置")]
        public int CatcherMaxSize { get; set; } = 20480000;

        /// <summary>抓取图片格式显示</summary>
        [Description("抓取图片格式显示")]
        [Category("抓取远程图片配置")]
        public string CatcherAllowFiles { get; set; } = ".png,.jpg,.jpeg,.gif,.bmp";
        #endregion

        #region 上传视频配置
 
        /// <summary>上传保存路径,可以自定义保存路径和文件名格式</summary>
        [Description("上传保存路径。可以自定义保存路径和文件名格式")]
        [Category("上传视频配置")]
        public string VideoPathFormat { get; set; } = "{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>上传大小限制，单位B，默认100MB</summary>
        [Description("上传大小限制。单位B，默认100MB")]
        [Category("上传视频配置")]
        public int VideoMaxSize { get; set; } = 204800000;

        /// <summary>上传视频格式显示</summary>
        [Description("上传视频格式显示")]
        [Category("上传视频配置")]
        public string VideoAllowFiles { get; set; } = ".flv,.swf,.mkv,.avi,.rm,.rmvb,.mpeg,.mpg,.ogg,.ogv,.mov,.wmv,.mp4,.webm,.mp3,.wav,.mid";
       
        #endregion

        #region 上传文件配置

        /// <summary>上传保存路径。可以自定义保存路径和文件名格式</summary>
        [Description("上传保存路径。可以自定义保存路径和文件名格式")]
        [Category("上传文件配置")]
        public string FilePathFormat { get; set; } = "{yyyy}{mm}{dd}/{time}{rand:6}";

        /// <summary>上传大小限制。单位B，默认50MB</summary>
        [Description("上传大小限制。单位B，默认50MB")]
        [Category("上传文件配置")]
        public int FileMaxSize { get; set; } = 204800000;

        /// <summary>上传文件格式显示</summary>
        [Description("上传文件格式显示")]
        [Category("上传文件配置")]
        public string FileAllowFiles { get; set; } = ".png,.jpg,.jpeg,.gif,.bmp.flv,.swf,.mkv,.avi,.rm,.rmvb,.mpeg,.mpg.ogg,.ogv,.mov,.wmv,.mp4,.webm,.mp3,.wav,.mid,.rar,.zip,.tar,.gz,.7z,.bz2,.cab,.iso,.doc,.docx,.xls,.xlsx,.ppt,.pptx,.pdf,.txt,.md,.xml";
        #endregion

        #region 列出指定目录下的图片
        /// <summary>每次列出文件数量</summary>
        [Description("每次列出文件数量")]
        [Category("列出指定目录下的图片")]
        public int ImageManagerListSize { get; set; } = 20;

        /// <summary>插入的图片浮动方式</summary>
        [Description("插入的图片浮动方式")]
        [Category("列出指定目录下的图片")]
        public string ImageManagerInsertAlign { get; set; } = "none";

        /// <summary>列出的文件类型</summary>
        [Description("列出的文件类型")]
        [Category("列出指定目录下的图片")]
        public string ImageManagerAllowFiles { get; set; } = ".png,.jpg,.jpeg,.gif,.bmp";
        #endregion

        #region 列出指定目录下的文件
        /// <summary>每次列出文件数量</summary>
        [Description("每次列出文件数量")]
        [Category("列出指定目录下的文件")]
        public int FileManagerListSize { get; set; } = 20;

        /// <summary>列出的文件类型</summary>
        [Description("列出指定目录下的文件")]
        [Category("列出指定目录下的文件")]
        public string FileManagerAllowFiles { get; set; } = ".png,.jpg,.jpeg,.gif,.bmp,.flv,.swf,.mkv,.avi,.rm,.rmvb,.mpeg,.mpg,.ogg,.ogv,.mov,.wmv,.mp4,.webm,.mp3,.wav,.mid,.rar,.zip,.tar,.gz,.7z,.bz2,.cab,.iso,.doc,.docx,.xls,.xlsx,.ppt,.pptx,.pdf,.txt,.md,.xml";
       
        #endregion
    }
}
