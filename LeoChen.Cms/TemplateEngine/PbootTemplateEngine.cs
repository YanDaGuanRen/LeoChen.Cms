using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using LeoChen.Cms.Data;
using NewLife.Caching;
using NewLife.Cube.Entity;
using NewLife.Reflection;
using XCode;

namespace LeoChen.Cms.TemplateEngine;

/// <summary>
/// 静态类版PBOOTCMS模板引擎
/// 特性：全程StringBuilder、复用ICacheProvider、从缓存获取数据、无临时数据存储
/// </summary>
public class PbootTemplateEngine : ITemplateEngine
{
    #region 依赖与配置

    public PbootTemplateEngine(TemplateEngineCache templateEngineCache, IHttpContextAccessor httpContextAccessor)
    {
        _templateEngineCache = templateEngineCache;
        _httpContextAccessor = httpContextAccessor;
    }
    
    // HTTP上下文访问器（用于请求信息）
    private readonly TemplateEngineCache _templateEngineCache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    // 匹配包含标签 {include file="header.html"}{include file='header.html'}{include file=header.html}
    // 捕获组：1-文件名 2-参数
    private readonly Regex RegexInclude = new("""\{include\s+file\s*=\s*["']?([^"'\s}]*)["']?\s*\}""", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
    // 匹配输出地址标签 {url.xxx}
    // 捕获组：1-地址
    private readonly Regex RegexOutputUrl = new("""/\{url\.([^\}]+)\}/""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    // 匹配输出地址标签 {homeurl.xxx}
    // 捕获组：1-地址
    private readonly Regex RegexOutputHomeUrl = new("""/\{homeurl\.([^\}]+)\}/""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    // 捕获组：1-列表变量名 2-项变量名 3-起始索引 4-显示数量 5-循环内部内容
    private readonly Regex RegexLoop = new(@"\{loop\s+(\$\w+)\s+(\$\w+)\s*(\d*)\s*(\d*)\}(.*?)\{/loop\}",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    // 匹配if条件标签 {if condition}...(直到elseif/else/{/if})
    // 捕获组：1-条件表达式 2-if块内容
    private readonly Regex RegexIf = new(@"\{if\s+(.*?)\}(.*?)(?=\{elseif|\{else|\{/if\})",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    // 匹配elseif条件标签 {elseif condition}...(直到elseif/else/{/if})
    // 捕获组：1-条件表达式 2-elseif块内容
    private readonly Regex RegexElseIf = new(@"\{elseif\s+(.*?)\}(.*?)(?=\{elseif|\{else|\{/if\})",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    // 匹配else标签 {else}...{/if}
    // 捕获组：1-else块内容
    private readonly Regex RegexElse = new(@"\{else\}(.*?)\{/if\}",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    // 匹配字段标签 {field:name} 或 {field:article.title length=20}
    // 捕获组：1-字段名(支持多级如article.title) 3-附加属性(如length=20)
    private readonly Regex RegexField =
        new(@"\{field:(\w+(\.\w+)?)(\s+[^}]*)?\}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // 匹配变量标签 {{variable}} 或 {{variable|filter}}
    // 捕获组：1-变量名(支持多级) 3-过滤器(如substr:20)
    private readonly Regex RegexVariable = new(@"\{\{([\w.]+)(\s*\|\s*([\w:]+))?\}\}",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // 匹配函数标签 {:func(param1, param2)}
    // 捕获组：1-函数名 2-函数参数
    private readonly Regex RegexFunction = new(@"\{:(\w+)\((.*?)\)\}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // 匹配分页导航标签 {pagenav num=5 style=1}
    // 捕获组：1-分页属性(如num=5,style=1)
    private readonly Regex RegexPageNav = new(@"\{pagenav(.*?)\}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    #endregion
  

    #region 核心解析入口

    /// <summary>
    /// 解析模板文件（主入口）
    /// </summary>
    public string ParseTemplate(string templatePath,string url, CmsArea cmsArea, CmsSite cmsSite, CmsCompany cmsCompany, CmsContent cmsContent = null)
    {
        if (string.IsNullOrEmpty(templatePath)) return "<!-- 模板路径不能为空 -->";

        try
        {
            var templateContent = LoadTemplateFile(cmsArea,templatePath);
            var contentSb = new StringBuilder(templateContent);
            
            ParseparOutputUrl(contentSb);
            ParseparOutputHomeUrl(contentSb);
            ParseOutputDefine(contentSb);
            ParseIncludeTag(cmsArea,contentSb); // 包含标签（优先解析，避免嵌套问题）
            PraseOutputVar(contentSb); // 输出变量
            PraseOutputObjVal(contentSb); // 输出对象
            PraseOutputConfig(contentSb); // 输出配置参数
            PraseOutputSession(contentSb); // 输出会话Session
            PraseOutputCookie(contentSb); // 输出会话Cookie
            PraseOutputServer(contentSb); // 输出环境变量
            PraseOutputPost(contentSb); // 输出POST请求值
            PraseOutputGet(contentSb); // 输出GET请求值
            PraseOutputArrVal(contentSb); // 输出数组
            PraseOutputFun(contentSb); // 使用函数
            // ParsePageNavTag(contentSb); // 分页标签
            // ParseLoopTag(contentSb); // 循环标签
            // ParseIfElseTag(contentSb); // 条件标签（if/elseif/else）
            // ParseFieldTag(contentSb); // 字段标签（{field:xxx}）
            // ParseVariableTag(contentSb); // 变量标签（{{xxx}}）
            // ParseFunctionTag(contentSb); // 函数标签（{:func()}）
            // ParseSpecialTag(contentSb); // 特殊标签（{:sitename}）
            // CleanEmptyTags(contentSb);
            return contentSb.ToString();;
        }
        catch (Exception ex)
        {
            return $"<!-- 模板解析错误：{ex.Message} -->";
        }
    }
    
    
    
    
    // 输出常量标签
    // 捕获组：1-常量名称
    private readonly Regex RegexOutputDefine = new("""/\{([A-Z_]+)\}/""", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
    /// <summary>
    /// 解析输出常量 如：{DB_HOST}
    /// </summary>
    /// <param name="contentSb"></param>
    private void ParseOutputDefine(StringBuilder contentSb)
    {
        //todo
        
        // string content = contentSb.ToString();
        // var matches = RegexOutputDefine.Matches(content);
        // for (int i = matches.Count - 1; i >= 0; i--)
        // {
        //     Match match = matches[i];
        //     string url = match.Groups[1].Value.Trim();
        //     contentSb.Replace(match.Value, url, match.Index, match.Length);
        // }
    }


    /// <summary>
    /// 输出地址 {url./admin/index/index}
    /// </summary>
    /// <param name="contentSb"> 内容</param>
    private void ParseparOutputUrl(StringBuilder contentSb)
    {
        //todo
        // string content = contentSb.ToString();
        // var matches = RegexOutputUrl.Matches(content);
        // for (int i = matches.Count - 1; i >= 0; i--)
        // {
        //     Match match = matches[i];
        //     string url = match.Groups[1].Value.Trim();
        //     contentSb.Replace(match.Value, url, match.Index, match.Length);
        // }
        
    }
    
    /// <summary>
    /// 输出地址 {homeurl./home/index/index}
    /// </summary>
    /// <param name="contentSb"> 内容</param>
    private void ParseparOutputHomeUrl(StringBuilder contentSb)
    {
        //todo
        // string content = contentSb.ToString();
        // var matches = RegexOutputHomeUrl.Matches(content);
        // for (int i = matches.Count - 1; i >= 0; i--)
        // {
        //     Match match = matches[i];
        //     string url = match.Groups[1].Value.Trim();
        //     contentSb.Replace(match.Value, url, match.Index, match.Length);
        // }
    }

    /// <summary>
    /// 加载模板文件内容
    /// </summary>
    private string LoadTemplateFile(CmsArea cmsArea,string templatePath)
    {
        _templateEngineCache.GetTemplatePath(cmsArea.ID, out var path);
        var fullPath =path.CombinePath(templatePath).GetFullPath();
        return File.Exists(fullPath) ? System.IO.File.ReadAllText(fullPath) :  $"<!--在目录:{path}下,模板文件不存在：{templatePath}-->";;
    }


    /// <summary>
    /// 清理空标签（修复了StringBuilder没有IndexOf的问题）
    /// </summary>
    private void CleanEmptyTags(StringBuilder contentSb)
    {
        if (contentSb == null || contentSb.Length == 0) return;

        // 先将StringBuilder转换为字符串进行查找操作
        string content = contentSb.ToString();

        // 清理未解析的变量标签
        contentSb.Replace("{{", "<!-- {{").Replace("}}", "}} -->");

        // 清理未解析的循环/条件标签（修复了标签列表和查找方式）
        var tagsToClean = new[] { "{loop", "{/loop", "{if", "{elseif", "{else", "{/if", "{field:", "{:" };
        foreach (var tag in tagsToClean)
        {
            int index = content.IndexOf(tag, StringComparison.OrdinalIgnoreCase);
            while (index != -1)
            {
                // 找到标签结束位置
                int endIndex = content.IndexOf("}", index) + 1;
                if (endIndex > index)
                {
                    string tagContent = content.Substring(index, endIndex - index);
                    contentSb.Replace(tagContent, $"<!-- {tagContent} -->", index, endIndex - index);

                    // 更新内容字符串，避免重复处理
                    content = contentSb.ToString();
                }

                // 查找下一个标签
                index = content.IndexOf(tag, index + 1, StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    #endregion

    #region 标签解析实现
    /// <summary>
    /// 解析包含标签 {include file="header.html"}
    /// </summary>
    /// <param name="cmsArea"></param>
    /// <param name="contentSb"></param>
    /// <param name="includedFiles"></param>
    private void ParseIncludeTag(CmsArea cmsArea, StringBuilder contentSb, HashSet<string> includedFiles = null)
    {
        // 初始化已包含文件集合
        if (includedFiles == null)
        {
            includedFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }
    
        string content = contentSb.ToString();
        var matches = RegexInclude.Matches(content);
        for (int i = matches.Count - 1; i >= 0; i--)
        {
            Match match = matches[i];
            // 组2是文件路径
            string includePath = match.Groups[1].Value.Trim();
        
            // 检查是否循环嵌套
            if (includedFiles.Contains(includePath))
            {
                contentSb.Replace(match.Value, $"<!-- 循环嵌套警告：文件 {includePath} 已被包含 -->", match.Index, match.Length);
                continue;
            }
        
            // 将当前文件添加到已包含集合
            var newIncludedFiles = new HashSet<string>(includedFiles, StringComparer.OrdinalIgnoreCase)
            {
                includePath
            };
        
            // 加载并解析包含的模板
            string includeContent = LoadTemplateFile(cmsArea, includePath);
            var includeSb = new StringBuilder(includeContent);

            // 递归解析包含模板的内部标签
            ParseIncludeTag(cmsArea, includeSb, newIncludedFiles);
            contentSb.Replace(match.Value, includeSb.ToString(), match.Index, match.Length);
        }
    }

    /// <summary>
    /// 解析循环标签 {loop $list $item [start] [length]}
    /// 支持内置变量：{{i}}（1开始）、{{i0}}（0开始）、{{first}}、{{last}}、{{odd}}、{{even}}
    /// </summary>
    private void ParseLoopTag(StringBuilder contentSb)
    {
        string content = contentSb.ToString();
        var matches = RegexLoop.Matches(content);

        for (int i = matches.Count - 1; i >= 0; i--)
        {
            Match match = matches[i];
            string listKey = match.Groups[1].Value.TrimStart('$'); // 列表键（如"articles"）
            string itemKey = match.Groups[2].Value.TrimStart('$'); // 项键（如"item"）
            int start = match.Groups[3].Value.ToInt(0); // 起始索引（默认0）
            int length = match.Groups[4].Value.ToInt(0); // 显示数量（默认全部）
            string innerContent = match.Groups[5].Value; // 循环内部内容

            // 1. 从缓存获取列表数据
            // IEnumerable listData = GetData(listKey) as IEnumerable;
            IEnumerable listData = null;
            if (listData == null)
            {
                contentSb.Replace(match.Value, $"<!-- 循环数据不存在：{listKey} -->", match.Index, match.Length);
                continue;
            }

            // 2. 拼接循环结果（StringBuilder优化）
            var loopSb = new StringBuilder();
            int index = 0; // 实际索引（0开始）
            int renderCount = 0; // 已渲染数量

            foreach (object item in listData)
            {
                // 跳过起始索引前的项
                if (index < start)
                {
                    index++;
                    continue;
                }

                // 达到数量限制则停止
                if (length > 0 && renderCount >= length)
                    break;

                // 3. 替换循环内置变量
                var itemSb = new StringBuilder(innerContent)
                    .Replace($"${itemKey}.", $"{itemKey}.") // 将$item.title转为item.title
                    .Replace("{{i}}", (index + 1).ToString()) // 1开始的索引
                    .Replace("{{i0}}", index.ToString()) // 0开始的索引
                    .Replace("{{first}}", (index == 0).ToString().ToLower()) // 是否第一项
                    .Replace("{{last}}", IsLastItem(listData, index).ToString().ToLower()) // 是否最后一项
                    .Replace("{{odd}}", (index % 2 == 0).ToString().ToLower()) // 是否奇数行
                    .Replace("{{even}}", (index % 2 == 1).ToString().ToLower()); // 是否偶数行

                // 4. 临时设置当前项到数据提供器上下文
                using (var context = new LoopItemContext(itemKey, item))
                {
                    ParseVariableTag(itemSb); // 解析项内变量
                    ParseIfElseTag(itemSb); // 解析项内条件
                }

                // 5. 添加到循环结果
                loopSb.Append(itemSb);
                index++;
                renderCount++;
            }

            // 6. 替换原循环标签
            contentSb.Replace(match.Value, loopSb.ToString(), match.Index, match.Length);
        }
    }

    /// <summary>
    /// 循环项上下文（临时设置当前项）
    /// </summary>
    private class LoopItemContext : IDisposable
    {
        private readonly string _itemKey;

        public LoopItemContext(string itemKey, object itemValue)
        {
            _itemKey = itemKey;
            LoopItemStorage.SetItem(itemKey, itemValue);
        }

        public void Dispose()
        {
            LoopItemStorage.RemoveItem(_itemKey);
        }
    }

    /// <summary>
    /// 循环项存储（线程安全，修复了静态类实例化问题）
    /// </summary>
    private static class LoopItemStorage
    {
        private static readonly ThreadLocal<Dictionary<string, object>> _threadItems = new ThreadLocal<Dictionary<string, object>>(() => new Dictionary<string, object>());

        public static void SetItem(string key, object value)
        {
            _threadItems.Value[key] = value;
        }

        public static object GetItem(string key)
        {
            if (_threadItems.Value.TryGetValue(key, out object value))
                return value;
            return null;
        }

        public static void RemoveItem(string key)
        {
            if (_threadItems.Value.ContainsKey(key))
                _threadItems.Value.Remove(key);
        }
    }

    /// <summary>
    /// 解析条件标签（if/elseif/else）
    /// </summary>
    private void ParseIfElseTag(StringBuilder contentSb)
    {
        // 先解析if标签
        ParseIfTag(contentSb);
        // 再解析elseif标签
        ParseElseIfTag(contentSb);
        // 最后解析else标签
        ParseElseTag(contentSb);
    }

    private void ParseIfTag(StringBuilder contentSb)
    {
        string content = contentSb.ToString();
        var matches = RegexIf.Matches(content);

        for (int i = matches.Count - 1; i >= 0; i--)
        {
            Match match = matches[i];
            string condition = match.Groups[1].Value.Trim();
            string ifContent = match.Groups[2].Value;

            // 评估条件
            bool isTrue = EvaluateCondition(condition);
            string replacement = isTrue ? ifContent : "";

            // 替换原if标签内容
            contentSb.Replace(match.Value, replacement, match.Index, match.Length);
        }
    }

    private void ParseElseIfTag(StringBuilder contentSb)
    {
        string content = contentSb.ToString();
        var matches = RegexElseIf.Matches(content);

        for (int i = matches.Count - 1; i >= 0; i--)
        {
            Match match = matches[i];
            string condition = match.Groups[1].Value.Trim();
            string elseIfContent = match.Groups[2].Value;

            bool isTrue = EvaluateCondition(condition);
            string replacement = isTrue ? elseIfContent : "";

            contentSb.Replace(match.Value, replacement, match.Index, match.Length);
        }
    }

    private void ParseElseTag(StringBuilder contentSb)
    {
        string content = contentSb.ToString();
        var matches = RegexElse.Matches(content);

        for (int i = matches.Count - 1; i >= 0; i--)
        {
            Match match = matches[i];
            string elseContent = match.Groups[1].Value;

            // else标签默认显示内容（前提是前面的if/elseif都不成立）
            contentSb.Replace(match.Value, elseContent, match.Index, match.Length);
        }
    }

    /// <summary>
    /// 解析字段标签 {field:title}、{field:article.content}
    /// </summary>
    private void ParseFieldTag(StringBuilder contentSb)
    {
        string content = contentSb.ToString();
        var matches = RegexField.Matches(content);

        for (int i = matches.Count - 1; i >= 0; i--)
        {
            Match match = matches[i];
            string fieldKey = match.Groups[1].Value.Trim(); // 如"title"、"article.content"
            string attrs = match.Groups[3].Value.Trim(); // 如"length=20"

            // 获取字段值
            // object fieldValue = GetData(fieldKey);
            object fieldValue = null;
            if (fieldValue == null)
            {
                contentSb.Replace(match.Value, "", match.Index, match.Length);
                continue;
            }

            // 处理字段属性（如length=20）
            string valueStr = fieldValue.ToString();
            if (!string.IsNullOrEmpty(attrs) && attrs.Contains("length="))
            {
                int length = attrs.Split('=')[1].ToInt(20);
                if (valueStr.Length > length)
                    valueStr = valueStr.Substring(0, length) + "...";
            }

            // 替换字段标签
            contentSb.Replace(match.Value, valueStr, match.Index, match.Length);
        }
    }

    /// <summary>
    /// 解析变量标签 {{xxx}}、{{xxx|filter}}
    /// </summary>
    private void ParseVariableTag(StringBuilder contentSb)
    {
        string content = contentSb.ToString();
        var matches = RegexVariable.Matches(content);

        for (int i = matches.Count - 1; i >= 0; i--)
        {
            Match match = matches[i];
            string varKey = match.Groups[1].Value.Trim(); // 变量键（如"site.name"）
            string filterStr = match.Groups[3].Value.Trim(); // 过滤器（如"substr:20"）

            // 获取变量值
            // object varValue = GetData(varKey);
             object varValue = null;
            if (varValue == null)
            {
                // 检查是否是循环项变量（如item.title）
                if (varKey.Contains("."))
                {
                    string[] parts = varKey.Split('.', 2);
                    object itemValue = LoopItemStorage.GetItem(parts[0]);
                    if (itemValue != null)
                    {
                        // varValue = GetPropertyValue(itemValue, parts[1]);
                        // todo 
                        varValue = "GetPropertyValue(itemValue, parts[1])";
                    }
                }
                else
                {
                    // 直接检查循环项
                    varValue = LoopItemStorage.GetItem(varKey);
                }
            }

            if (varValue == null)
            {
                contentSb.Replace(match.Value, "", match.Index, match.Length);
                continue;
            }

            // 应用过滤器
            string valueStr = ApplyFilter(varValue, filterStr);

            // 替换变量标签
            contentSb.Replace(match.Value, valueStr, match.Index, match.Length);
        }
    }

    /// <summary>
    /// 解析函数标签 {::url('article/1.html')}、{:substr($title,20)}
    /// 修复了方法实现，无需返回值因为直接操作StringBuilder
    /// </summary>
    private void ParseFunctionTag(StringBuilder contentSb)
    {
        string content = contentSb.ToString();
        var matches = RegexFunction.Matches(content);

        for (int i = matches.Count - 1; i >= 0; i--)
        {
            Match match = matches[i];
            string funcName = match.Groups[1].Value.Trim().ToLower(); // 函数名
            string funcParam = match.Groups[2].Value.Trim(); // 函数参数

            // 解析参数（支持变量替换）
            var paramSb = new StringBuilder(funcParam);
            ParseVariableTag(paramSb);
            string parsedParam = paramSb.ToString().Trim('\'', '"');

            // 执行函数逻辑
            string result;
            switch (funcName)
            {
                case "url":
                    // result = $"{GetData("site.url")}{parsedParam}";
                    // todo
                    result = """$\\\"{GetData(\\\"site.url\\\")}{parsedParam}\\\""";
                    
                    break;
                case "thumb":
                    // result = $"{GetData("site.url")}upload/thumb/{parsedParam}";
                    result = """$"{GetData("site.url")}upload/thumb/{parsedParam}""";
                    break;
                case "substr":
                    string[] substrParts = parsedParam.Split(',');
                    if (substrParts.Length >= 2)
                    {
                        string str = substrParts[0].Trim('\'', '"');
                        int length = substrParts[1].ToInt(20);
                        result = str.Length > length ? str.Substring(0, length) + "..." : str;
                    }
                    else
                    {
                        result = parsedParam;
                    }

                    break;
                case "strlen":
                    result = parsedParam.Length.ToString();
                    break;
                case "date":
                    string[] dateParts = parsedParam.Split(',');
                    string date = dateParts.Length >= 1 ? dateParts[0].Trim() : DateTime.Now.ToString();
                    string format = dateParts.Length >= 2 ? dateParts[1].Trim('\'', '"') : "yyyy-MM-dd";
                    result = DateTime.TryParse(date, out DateTime dt) ? dt.ToString(format) : date;
                    break;
                default:
                    result = $"<!-- 未知函数：{funcName} -->";
                    break;
            }

            // 替换函数标签
            contentSb.Replace(match.Value, result, match.Index, match.Length);
        }
    }

    /// <summary>
    /// 解析分页标签 {pagenav num=5 style=1}
    /// </summary>
    private void ParsePageNavTag(StringBuilder contentSb)
    {
        string content = contentSb.ToString();
        var matches = RegexPageNav.Matches(content);

        for (int i = matches.Count - 1; i >= 0; i--)
        {
            Match match = matches[i];
            string attrs = match.Groups[1].Value.Trim();

            // 从缓存获取分页信息
            // PageInfo pageInfo = GetData("pageinfo") as PageInfo;
            // todo
            PageInfo pageInfo = null;   
            if (pageInfo == null)
            {
                contentSb.Replace(match.Value, "<!-- 未设置分页信息（需从缓存提供pageinfo数据） -->", match.Index, match.Length);
                continue;
            }

            // 解析分页属性
            int showNum = 5; // 默认显示5个页码
            if (attrs.Contains("num="))
                showNum = Regex.Match(attrs, @"num=(\d+)").Groups[1].Value.ToInt(5);

            int style = 1; // 默认样式
            if (attrs.Contains("style="))
                style = Regex.Match(attrs, @"style=(\d+)").Groups[1].Value.ToInt(1);

            // 生成HTML（StringBuilder拼接）
            var pageSb = new StringBuilder();
            pageSb.Append($"<div class=\"pagination pagination-style-{style}\">");

            // 上一页
            if (pageInfo.CurrentPage > 1)
            {
                string prevUrl = ReplacePageNum(pageInfo.UrlTemplate, pageInfo.CurrentPage - 1);
                pageSb.AppendFormat("<a href=\"{0}\" class=\"page-prev\">上一页</a>", prevUrl);
            }
            else
            {
                pageSb.Append("<span class=\"page-prev disabled\">上一页</span>");
            }

            // 计算页码范围
            int startPage = Math.Max(1, pageInfo.CurrentPage - showNum / 2);
            int endPage = Math.Min(pageInfo.TotalPages, startPage + showNum - 1);
            // 调整起始页（确保显示数量）
            if (endPage - startPage + 1 < showNum && startPage > 1)
                startPage = Math.Max(1, endPage - showNum + 1);

            // 首页
            if (startPage > 1)
            {
                string firstUrl = ReplacePageNum(pageInfo.UrlTemplate, 1);
                pageSb.AppendFormat("<a href=\"{0}\" class=\"page-item\">1</a>", firstUrl);
                if (startPage > 2)
                    pageSb.Append("<span class=\"page-ellipsis\">...</span>");
            }

            // 中间页码
            for (int p = startPage; p <= endPage; p++)
            {
                if (p == pageInfo.CurrentPage)
                {
                    pageSb.AppendFormat("<span class=\"page-item current\">{0}</span>", p);
                }
                else
                {
                    string pageUrl = ReplacePageNum(pageInfo.UrlTemplate, p);
                    pageSb.AppendFormat("<a href=\"{1}\" class=\"page-item\">{0}</a>", p, pageUrl);
                }
            }

            // 末页
            if (endPage < pageInfo.TotalPages)
            {
                if (endPage < pageInfo.TotalPages - 1)
                    pageSb.Append("<span class=\"page-ellipsis\">...</span>");
                string lastUrl = ReplacePageNum(pageInfo.UrlTemplate, pageInfo.TotalPages);
                pageSb.AppendFormat("<a href=\"{0}\" class=\"page-item\">{1}</a>", lastUrl, pageInfo.TotalPages);
            }

            // 下一页
            if (pageInfo.CurrentPage < pageInfo.TotalPages)
            {
                string nextUrl = ReplacePageNum(pageInfo.UrlTemplate, pageInfo.CurrentPage + 1);
                pageSb.AppendFormat("<a href=\"{0}\" class=\"page-next\">下一页</a>", nextUrl);
            }
            else
            {
                pageSb.Append("<span class=\"page-next disabled\">下一页</span>");
            }

            // 分页信息文本
            pageSb.AppendFormat("<span class=\"page-info\">共{0}页/{1}条</span>", pageInfo.TotalPages,
                pageInfo.TotalCount);
            pageSb.Append("</div>");

            // 替换分页标签
            contentSb.Replace(match.Value, pageSb.ToString(), match.Index, match.Length);
        }
    }

    /// <summary>
    /// 解析特殊标签 {sitename}、{webname}、{year}等
    /// </summary>
    private void ParseSpecialTag(StringBuilder contentSb)
    {
        // PBOOTCMS常用特殊标签映射
        var specialTags = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "{sitename}", "{{site.name}}" },
            { "{webname}", "{{config.webname}}" },
            { "{keywords}", "{{config.keywords}}" },
            { "{description}", "{{config.description}}" },
            { "{copyright}", "{{config.copyright}}" },
            { "{icp}", "{{site.icp}}" },
            { "{year}", "{{time.year}}" },
            { "{month}", "{{time.month}}" },
            { "{day}", "{{time.day}}" },
            { "{now}", "{{time.now}}" },
            { "{siteurl}", "{{site.url}}" }
        };

        // 替换特殊标签为变量标签，再解析变量
        foreach (var kv in specialTags)
        {
            contentSb.Replace(kv.Key, kv.Value);
        }

        // 重新解析变量标签
        ParseVariableTag(contentSb);
    }

    #endregion

    #region 5. 辅助方法

    /// <summary>
    /// 评估条件表达式（支持 ==、!=、>、<、>=、<=、&&、||、!）
    /// </summary>
    private bool EvaluateCondition(string condition)
    {
        if (string.IsNullOrEmpty(condition)) return false;

        try
        {
            // 1. 替换条件中的变量（如$article.id → 实际值）
            var evalCondition = new StringBuilder(condition);
            ParseVariableTag(evalCondition);
            string parsedCondition = evalCondition.ToString()
                .Replace("&&", " AND ")
                .Replace("||", " OR ")
                .Replace("!", " NOT ")
                .Trim();

            // 2. 处理括号（简化版，仅支持一层）
            while (parsedCondition.Contains("(") && parsedCondition.Contains(")"))
            {
                int startIdx = parsedCondition.LastIndexOf("(");
                int endIdx = parsedCondition.IndexOf(")", startIdx);
                if (startIdx == -1 || endIdx == -1) break;

                string innerCond = parsedCondition.Substring(startIdx + 1, endIdx - startIdx - 1);
                string innerResult = EvaluateSimpleCondition(innerCond) ? "true" : "false";
                parsedCondition = parsedCondition.Replace(
                    parsedCondition.Substring(startIdx, endIdx - startIdx + 1),
                    innerResult
                );
            }

            // 3. 评估简单条件
            return EvaluateSimpleCondition(parsedCondition);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 评估简单条件（无括号，仅一层逻辑运算）
    /// 修复了Split方法的使用问题
    /// </summary>
    private bool EvaluateSimpleCondition(string condition)
    {
        // 处理逻辑与（AND）
        if (condition.IndexOf("AND", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            string[] parts = Regex.Split(condition, @"\s+AND\s+", RegexOptions.IgnoreCase);
            foreach (string part in parts)
            {
                if (!EvaluateComparison(part.Trim()))
                    return false;
            }

            return true;
        }

        // 处理逻辑或（OR）
        if (condition.IndexOf("OR", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            string[] parts = Regex.Split(condition, @"\s+OR\s+", RegexOptions.IgnoreCase);
            foreach (string part in parts)
            {
                if (EvaluateComparison(part.Trim()))
                    return true;
            }

            return false;
        }

        // 处理单条件比较
        return EvaluateComparison(condition);
    }

    /// <summary>
    /// 评估比较表达式（==、!=、>、<、>=、<=）
    /// </summary>
    private bool EvaluateComparison(string condition)
    {
        condition = condition.Trim();
        if (string.IsNullOrEmpty(condition)) return false;

        // 处理非运算（NOT）
        bool isNot = false;
        if (condition.StartsWith("NOT", StringComparison.OrdinalIgnoreCase))
        {
            isNot = true;
            condition = condition.Substring(3).Trim();
        }

        // 支持的比较运算符（按优先级排序）
        string[] operators = new[] { ">=", "<=", "==", "!=", ">", "<" };
        foreach (string op in operators)
        {
            if (condition.Contains(op))
            {
                string[] parts = condition.Split(new[] { op }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) return isNot ? true : false;

                string left = parts[0].Trim().Trim('\'', '"');
                string right = parts[1].Trim().Trim('\'', '"');

                // 比较结果
                bool result;
                if (double.TryParse(left, out double leftNum) && double.TryParse(right, out double rightNum))
                {
                    // 数值比较
                    result = op switch
                    {
                        ">=" => leftNum >= rightNum,
                        "<=" => leftNum <= rightNum,
                        "==" => leftNum == rightNum,
                        "!=" => leftNum != rightNum,
                        ">" => leftNum > rightNum,
                        "<" => leftNum < rightNum,
                        _ => false
                    };
                }
                else if (DateTime.TryParse(left, out DateTime leftDate) &&
                         DateTime.TryParse(right, out DateTime rightDate))
                {
                    // 日期比较
                    result = op switch
                    {
                        ">=" => leftDate >= rightDate,
                        "<=" => leftDate <= rightDate,
                        "==" => leftDate == rightDate,
                        "!=" => leftDate != rightDate,
                        ">" => leftDate > rightDate,
                        "<" => leftDate < rightDate,
                        _ => false
                    };
                }
                else
                {
                    // 字符串比较
                    result = op switch
                    {
                        ">=" => string.Compare(left, right) >= 0,
                        "<=" => string.Compare(left, right) <= 0,
                        "==" => string.Equals(left, right, StringComparison.OrdinalIgnoreCase),
                        "!=" => !string.Equals(left, right, StringComparison.OrdinalIgnoreCase),
                        ">" => string.Compare(left, right) > 0,
                        "<" => string.Compare(left, right) < 0,
                        _ => false
                    };
                }

                // 应用非运算
                return isNot ? !result : result;
            }
        }

        // 非比较表达式（如"true"、"false"、非空判断）
        if (bool.TryParse(condition, out bool boolResult))
            return isNot ? !boolResult : boolResult;

        // 非空判断（非空字符串视为true）
        return isNot ? string.IsNullOrEmpty(condition) : !string.IsNullOrEmpty(condition);
    }

    /// <summary>
    /// 应用变量过滤器
    /// </summary>
    private string ApplyFilter(object value, string filterStr)
    {
        if (value == null || string.IsNullOrEmpty(filterStr)) return value?.ToString() ?? "";
        string[] filterParts = filterStr.Split(':', 2);
        string filterName = filterParts[0].ToLower();
        string filterParam = filterParts.Length > 1 ? filterParts[1] : "";
        string valueStr = value.ToString();
        string result = valueStr; // 默认返回原始值
        switch (filterName)
        {
            case "date":
                string dateFormat = string.IsNullOrEmpty(filterParam) ? "yyyy-MM-dd" : filterParam;
                result = DateTime.TryParse(valueStr, out DateTime dateDt) ? dateDt.ToString(dateFormat) : valueStr;
                break;
            case "datetime":
                string datetimeFormat = string.IsNullOrEmpty(filterParam) ? "yyyy-MM-dd HH:mm:ss" : filterParam;
                result = DateTime.TryParse(valueStr, out DateTime datetimeDt)
                    ? datetimeDt.ToString(datetimeFormat)
                    : valueStr;
                break;
            case "substr":
                int substrLength = filterParam.ToInt(20);
                result = valueStr.Length > substrLength ? valueStr.Substring(0, substrLength) + "..." : valueStr;
                break;
            case "int": result = value.ToInt().ToString(); break;
            case "float": result = value.ToDouble().ToString("0.00"); break;
            case "lower": result = valueStr.ToLower(); break;
            case "upper": result = valueStr.ToUpper(); break;
            case "htmlspecialchars": result = System.Web.HttpUtility.HtmlEncode(valueStr); break;
            case "strip_tags": result = Regex.Replace(valueStr, @"<[^>]+>", ""); break;
            case "trim":
                result = valueStr.Trim();
                break;
        }

        return result;
    }

    /// <summary>
    /// 判断是否为列表最后一项
    /// </summary>
    private bool IsLastItem(IEnumerable list, int currentIndex)
    {
        if (list is ICollection collection)
            return currentIndex == collection.Count - 1;

        // 非集合类型，遍历计数
        int count = 0;
        foreach (var _ in list) count++;
        return currentIndex == count - 1;
    }

    /// <summary>
    /// 替换URL模板中的页码（如"/list-{page}.html" → "/list-2.html"）
    /// </summary>
    private string ReplacePageNum(string urlTemplate, int pageNum)
    {
        if (string.IsNullOrEmpty(urlTemplate))
            return $"?page={pageNum}";

        // 支持两种URL模板：/list-{page}.html 或 /list?page={page}
        if (urlTemplate.Contains("{page}"))
            return urlTemplate.Replace("{page}", pageNum.ToString());
        else if (urlTemplate.Contains("page="))
            return Regex.Replace(urlTemplate, "page=\\d+", $"page={pageNum}");
        else
            return $"{urlTemplate}{(urlTemplate.Contains("?") ? "&" : "?")}page={pageNum}";
    }

    #endregion

    #region 6. 分页信息类（PBOOTCMS分页标签专用）

    /// <summary>
    /// 分页信息类（需从缓存提供）
    /// </summary>
    public class PageInfo
    {
        /// <summary>当前页码（从1开始）</summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>每页条数</summary>
        public int PageSize { get; set; } = 10;

        /// <summary>总记录数</summary>
        public int TotalCount { get; set; } = 0;

        /// <summary>总页数</summary>
        public int TotalPages => TotalCount <= 0 ? 1 : (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>URL模板（含{page}占位符，如"/article/list-{page}.html"）</summary>
        public string UrlTemplate { get; set; } = "/list-{page}.html";
    }

    #endregion
}