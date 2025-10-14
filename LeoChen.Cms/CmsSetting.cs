using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using NewLife.Configuration;
using NewLife.Cube.Configuration;
using NewLife.Security;
using XCode.Configuration;

namespace LeoChen.Cms;


/// <summary>系统设置</summary>
[DisplayName("系统设置")]
[Config("CmsSetting")]
public class CmsSetting : Config<CmsSetting>
{
    static CmsSetting() => Provider = new CmsDBConfigProvider() { Category = "Cms" };
    
    #region 网站配置


    #endregion
    #region 基本配置
    /// <summary>网站状态</summary>
    [Description("关闭网站")]
    [Category("基本配置")]
    public Boolean CloseSite { get; set; } = false;
    
    /// <summary>关站提示</summary>
    [Description("关站提示")]
    [Category("基本配置")]
    public String CloseSiteNote { get; set; } = "网站维护中，请稍后访问...";
    
    /// <summary>动态缓存</summary>
    [Description("启用动态缓存")]
    [Category("基本配置")]
    public Boolean TplHtmlCache { get; set; } = false;
    
    /// <summary>缓存有效期(秒)</summary>
    [Description("缓存有效期")]
    [Category("基本配置")]
    public Int32 TplHtmlCacheTime { get; set; } = 900;
    
    /// <summary>Gzip页面压缩</summary>
    [Description("启用Gzip压缩")]
    [Category("基本配置")]
    public Boolean Gzip { get; set; } = false;
    
    
    /// <summary>跨语言自动切换</summary>
    [Description("启用跨语言自动切换")]
    [Category("基本配置")]
    public Boolean LgAutoSw { get; set; } = true;
    
    /// <summary>记录蜘蛛访问</summary>
    [Description("记录蜘蛛访问日志")]
    [Category("基本配置")]
    public Boolean SpiderLog { get; set; } = true;
    
    /// <summary>自动转HTTPS</summary>
    [Description("自动跳转到HTTPS")]
    [Category("基本配置")]
    public Boolean ToHttps { get; set; } = false;
    
    /// <summary>自动转主域名</summary>
    [Description("自动跳转到主域名")]
    [Category("基本配置")]
    public Boolean ToMainDomain { get; set; } = false;
    
    /// <summary>网站主域名</summary>
    [Description("主域名")]
    [Category("基本配置")]
    public String MainDomain { get; set; } = "";
    
    /// <summary>分页数字条数量</summary>
    [Description("分页数字条数量")]
    [Category("基本配置")]
    public Int32 PageNum { get; set; } = 5;
    
    /// <summary>内链替换次数</summary>
    [Description("内链替换次数")]
    [Category("基本配置")]
    public Int32 ContentTagsReplaceNum { get; set; } = 3;
    
    /// <summary>敏感词过滤</summary>
    [Description("敏感词列表")]
    [Category("基本配置")]
    public String ContentKeywordReplace { get; set; } = "";
    

    #endregion
    
    #region 邮件通知
    /// <summary>SMTP服务器</summary>
    [Description("SMTP服务器")]
    [Category("邮件通知")]
    public String SmtpServer { get; set; } = "";
    
    /// <summary>SMTP端口</summary>
    [Description("SMTP端口")]
    [Category("邮件通知")]
    public Int32 SmtpPort { get; set; } = 25;
    
    /// <summary>是否为SSL</summary>
    [Description("启用SSL连接")]
    [Category("邮件通知")]
    public Boolean SmtpSsl { get; set; } = false;
    
    /// <summary>邮箱账号</summary>
    [Description("邮箱账号")]
    [Category("邮件通知")]
    public String SmtpUsername { get; set; } = "";
    
    /// <summary>邮箱密码</summary>
    [Description("邮箱密码")]
    [Category("邮件通知")]
    public String SmtpPassword { get; set; } = "";
    
    /// <summary>测试账号</summary>
    [Description("测试邮箱账号")]
    [Category("邮件通知")]
    public String SmtpUsernameTest { get; set; } = "";
    
    /// <summary>留言发送邮件</summary>
    [Description("留言时发送邮件")]
    [Category("邮件通知")]
    public Boolean MessageSendMail { get; set; } = false;
    
    /// <summary>表单发送邮件</summary>
    [Description("表单提交时发送邮件")]
    [Category("邮件通知")]
    public Boolean FormSendMail { get; set; } = false;
    
    /// <summary>评论发送邮件</summary>
    [Description("评论时发送邮件")]
    [Category("邮件通知")]
    public Boolean CommentSendMail { get; set; } = false;
    
    /// <summary>信息接收邮箱</summary>
    [Description("信息接收邮箱")]
    [Category("邮件通知")]
    public String MessageSendTo { get; set; } = "";
    #endregion
    
    #region 百度接口
    /// <summary>普通收录token</summary>
    [Description("百度普通收录token")]
    [Category("百度接口")]
    public String BaiduZzToken { get; set; } = "";
    
    /// <summary>快速收录token</summary>
    [Description("百度快速收录token")]
    [Category("百度接口")]
    public String BaiduKsToken { get; set; } = "";
    #endregion
    
    #region 图片水印
    /// <summary>水印状态</summary>
    [Description("启用图片水印")]
    [Category("图片水印")]
    public Boolean WatermarkOpen { get; set; } = false;
    
    /// <summary>水印文字</summary>
    [Description("水印文字")]
    public String WatermarkText { get; set; } = "";
    
    /// <summary>文字字体</summary>
    [Description("水印文字字体")]
    public String WatermarkTextFont { get; set; } = "";
    
    /// <summary>文字大小</summary>
    [Description("水印文字大小")]
    public Int32 WatermarkTextSize { get; set; } = 20;
    
    /// <summary>文字颜色</summary>
    [Description("水印文字颜色")]
    public String WatermarkTextColor { get; set; } = "100,100,100";
    
    /// <summary>水印图片</summary>
    [Description("水印图片路径")]
    public String WatermarkPic { get; set; } = "";
    
    /// <summary>水印位置</summary>
    [Description("水印位置")]
    public Int32 WatermarkPosition { get; set; } = 5; // 1-左上, 2-右上, 3-左下, 4-右下, 5-中间
    #endregion
    
    #region 安全配置
    /// <summary>留言功能</summary>
    [Description("启用留言功能")]
    [Category("安全配置")]
    public Boolean MessageStatus { get; set; } = true;
    
    /// <summary>留言验证码</summary>
    [Description("留言时需要验证码")]
    [Category("安全配置")]
    public Boolean MessageCheckCode { get; set; } = true;
    
    /// <summary>留言审核</summary>
    [Description("留言需要审核")]
    [Category("安全配置")]
    public Boolean MessageVerify { get; set; } = true;
    
    /// <summary>留言需登录</summary>
    [Description("留言需要登录")]
    [Category("安全配置")]
    public Boolean MessageRqLogin { get; set; } = false;
    
    /// <summary>表单功能</summary>
    [Description("启用表单功能")]
    [Category("安全配置")]
    public Boolean FormStatus { get; set; } = true;
    
    /// <summary>表单验证码</summary>
    [Description("表单提交时需要验证码")]
    [Category("安全配置")]
    public Boolean FormCheckCode { get; set; } = true;
    
    /// <summary>后台登录阀值</summary>
    [Description("登录失败锁定阈值")]
    [Category("安全配置")]
    public Int32 LockCount { get; set; } = 5;
    
    /// <summary>失败锁定时间</summary>
    [Description("登录失败锁定时间(秒)")]
    [Category("安全配置")]
    public Int32 LockTime { get; set; } = 900;
    #endregion
    
    #region URL规则
    /// <summary>地址模式</summary>
    [Description("URL模式")]
    [Category("URL规则")]
    public Int32 UrlRuleType { get; set; } = 3; // 2-伪静态, 3-兼容模式
    
    /// <summary>文章路径</summary>
    [Description("文章路径模式")]
    [Category("URL规则")]
    public Int32 UrlRuleContentPath { get; set; } = 0; // 0-带栏目路径, 1-不带栏目路径
    
    /// <summary>404跳转</summary>
    [Description("404跳转设置")]
    [Category("URL规则")]
    public Int32 UrlIndex404 { get; set; } = 0; // 0-关闭404, 1-开启404
    #endregion
    
    #region 标题样式
    /// <summary>首页标题</summary>
    [Description("首页标题模板")]
    [Category("标题样式")]
    public String IndexTitle { get; set; } = "";
    
    /// <summary>专题页标题</summary>
    [Description("专题页标题模板")]
    [Category("标题样式")]
    public String AboutTitle { get; set; } = "";
    
    /// <summary>列表页标题</summary>
    [Description("列表页标题模板")]
    [Category("标题样式")]
    public String ListTitle { get; set; } = "";
    
    /// <summary>内容页标题</summary>
    [Description("内容页标题模板")]
    [Category("标题样式")]
    public String ContentTitle { get; set; } = "";
    
    /// <summary>搜索结果页标题</summary>
    [Description("搜索结果页标题模板")]
    [Category("标题样式")]
    public String SearchTitle { get; set; } = "";
    
    /// <summary>会员注册页标题</summary>
    [Description("会员注册页标题模板")]
    [Category("标题样式")]
    public String RegisterTitle { get; set; } = "";
    
    /// <summary>会员登录页标题</summary>
    [Description("会员登录页标题模板")]
    [Category("标题样式")]
    public String LoginTitle { get; set; } = "";
    
    /// <summary>个人中心页标题</summary>
    [Description("个人中心页标题模板")]
    [Category("标题样式")]
    public String UcenterTitle { get; set; } = "";
    
    /// <summary>资料修改页标题</summary>
    [Description("资料修改页标题模板")]
    [Category("标题样式")]
    public String UmodifyTitle { get; set; } = "";
    
    /// <summary>其它页标题</summary>
    [Description("其它页标题模板")]
    [Category("标题样式")]
    public String OtherTitle { get; set; } = "";
    #endregion
    
    #region 会员配置
    /// <summary>会员注册</summary>
    [Description("启用会员注册")]
    [Category("会员配置")]
    public Boolean RegisterStatus { get; set; } = true;
    
    /// <summary>会员注册类型</summary>
    [Description("会员注册方式")]
    [Category("会员配置")]
    public Int32 RegisterType { get; set; } = 1; // 1-用户名, 2-邮箱, 3-手机
    
    /// <summary>会员注册验证码</summary>
    [Description("注册时需要验证码")]
    [Category("会员配置")]
    public Int32 RegisterCheckCode { get; set; } = 1; // 0-禁用, 1-普通验证码, 2-邮箱验证码
    
    /// <summary>会员注册审核</summary>
    [Description("新用户注册需要审核")]
    [Category("会员配置")]
    public Boolean RegisterVerify { get; set; } = false;
    
    /// <summary>会员登录</summary>
    [Description("启用会员登录")]
    [Category("会员配置")]
    public Boolean LoginStatus { get; set; } = true;
    
    /// <summary>会员登录验证码</summary>
    [Description("登录时需要验证码")]
    [Category("会员配置")]
    public Boolean LoginCheckCode { get; set; } = true;
    
    /// <summary>不等待跳登录</summary>
    [Description("直接跳转到登录页")]
    [Category("会员配置")]
    public Boolean LoginNoWait { get; set; } = false;
    
    /// <summary>评论功能</summary>
    [Description("启用评论功能")]
    [Category("会员配置")]
    public Boolean CommentStatus { get; set; } = true;
    
    /// <summary>匿名评论</summary>
    [Description("允许匿名评论")]
    [Category("会员配置")]
    public Boolean CommentAnonymous { get; set; } = false;
    
    /// <summary>评论验证码</summary>
    [Description("评论时需要验证码")]
    [Category("会员配置")]
    public Boolean CommentCheckCode { get; set; } = true;
    
    /// <summary>评论审核</summary>
    [Description("评论需要审核")]
    [Category("会员配置")]
    public Boolean CommentVerify { get; set; } = true;
    
    /// <summary>会员注册积分</summary>
    [Description("注册初始积分")]
    [Category("会员配置")]
    public Int32 RegisterScore { get; set; } = 0;
    
    /// <summary>会员登录积分</summary>
    [Description("每日登录积分")]
    [Category("会员配置")]
    public Int32 LoginScore { get; set; } = 0;
    
    /// <summary>会员默认等级</summary>
    [Description("默认用户组编码")]
    [Category("会员配置")]
    public String RegisterGCode { get; set; } = "";
    
    /// <summary>允许上传格式</summary>
    [Description("允许上传的文件格式")]
    [Category("会员配置")]
    public String HomeUploadExt { get; set; } = "jpg,jpeg,png,gif,xls,xlsx,doc,docx,ppt,pptx,rar,zip,pdf,txt";
    #endregion
}
