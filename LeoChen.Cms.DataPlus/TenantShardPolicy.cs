using System;
using NewLife;
using XCode;
using XCode.Configuration;
using XCode.Membership;
using XCode.Shards;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NewLife.Data;
using XCode.DataAccessLayer;
using Expression = XCode.Expression;

namespace LenChen.Cms.Data;

/// <summary>租户分表分库策略</summary>
public class TenantShardPolicy : IShardPolicy
{
    #region 属性配置

    /// <summary>分片字段（默认TenantId）</summary>
    public FieldItem? Field { get; set; }

    /// <summary>实体工厂</summary>
    public IEntityFactory? Factory { get; set; }

    /// <summary>租户字段名（默认TenantId）</summary>
    public String TenantFieldName { get; set; } = "TenantId";

    /// <summary>默认数据库连接名</summary>
    public string DefaultConnName { get; set; }

    /// <summary>默认表名</summary>
    public string DefaultTable { get; set; }

    #endregion

    #region 构造函数

    /// <summary>初始化租户分片策略</summary>
    /// <param name="factory">实体工厂</param>
    /// <param name="tenantFieldName">租户字段名</param>
    public TenantShardPolicy(IEntityFactory factory, string tenantFieldName = "TenantId")
    {
        TenantFieldName = tenantFieldName;
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Field = Factory.Table.FindByName(tenantFieldName) ?? throw new ArgumentNullException($"{nameof(Factory)}中未找到{tenantFieldName}的字段");
        DefaultConnName = Factory.Table.ConnName.IsNullOrEmpty()? throw new ArgumentNullException($"{nameof(Factory)}中未找到连接名"):Factory.Table.ConnName;
        DefaultTable = Factory.Table.TableName.IsNullOrEmpty() ? throw new ArgumentNullException($"{nameof(Factory)}中未找到相应的表名"):Factory.Table.TableName;
    }


    /// <summary>初始化租户分片策略</summary>
    /// <param name="factory">实体工厂</param>
    /// <param name="tenantField">租户字段名</param>
    public TenantShardPolicy(FieldItem tenantField, IEntityFactory? factory = null)
    {
        Field = tenantField ?? throw new ArgumentNullException(nameof(tenantField));
        TenantFieldName = tenantField.Name;
        Factory = (factory ?? tenantField.Factory) ?? throw new ArgumentNullException($"{nameof(tenantField)}中未找到相应的工厂");
        DefaultConnName = Factory.Table.ConnName.IsNullOrEmpty()? throw new ArgumentNullException($"{nameof(Factory)}中未找到连接名"):Factory.Table.ConnName;
        DefaultTable = Factory.Table.TableName.IsNullOrEmpty() ? throw new ArgumentNullException($"{nameof(Factory)}中未找到相应的表名"):Factory.Table.TableName;
    }

    #endregion

    #region 单值分片（实体/租户ID）

    /// <summary>为实体或租户ID计算分片</summary>
    public ShardModel? Shard(Object value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        // 提取租户ID
        var tenantId = GetTenantIdFromValue(value);
        if (tenantId <= 0) return null;

        return GetShardModel(tenantId);
    }
    
    #endregion

    #region 表达式分片（解析查询条件）

    /// <summary>从查询表达式中解析租户条件，生成分片</summary>
    public ShardModel[] Shards(Expression expression)
    {
        var tenantIds = new HashSet<Int32>();
        if (TenantContext.CurrentId > 0)
            tenantIds.Add(TenantContext.CurrentId);
        // 递归解析表达式，提取租户ID
        CollectTenantIds(expression, tenantIds);
        // 为每个租户ID生成分片
        return tenantIds.Select(tenantId => GetShardModel(tenantId)).Where(e => e != null).ToArray();
    }

    #endregion

    #region 时间区间分片（租户策略暂不支持）

    /// <summary>时间区间分片（租户策略无需实现，返回空）</summary>
    public ShardModel[] Shards(DateTime start, DateTime end) => Array.Empty<ShardModel>();

    #endregion

    #region 私有方法

    /// <summary>
    /// 获取数据库连接字符串
    /// </summary>
    /// <param name="connName">数据库连接字符串名称</param>
    /// <param name="replaceConnName">新的库名称</param>
    /// <param name="connstr">连接字符串</param>
    /// <returns>是否成功</returns>
    private static bool GetConnStr(string connName, string replaceConnName, out string connstr)
    {
        connstr = string.Empty;
        if (string.IsNullOrEmpty(connName)) return false;
        if (!DAL.ConnStrs.TryGetValue(connName, out var oldconnstr)) return false;

        if (oldconnstr.StartsWith("MapTo=", StringComparison.OrdinalIgnoreCase))
        {
            return GetConnStr(oldconnstr.Replace("MapTo=", "", StringComparison.OrdinalIgnoreCase), replaceConnName,
                out connstr);
        }

        // 处理不同数据库类型的数据库名字段
        // SQLite
        if (oldconnstr.Contains("Data Source=", StringComparison.OrdinalIgnoreCase))
        {
            var sqliteRegex = new Regex(
                @"Data Source=(?<path>.+?)[\\/]?(?<filename>[^\\/;]+)\.db",
                RegexOptions.IgnoreCase
            );
            var match = sqliteRegex.Match(oldconnstr);
            if (!match.Success) return false;

            var path = match.Groups["path"].Value;
            var originalDataSource = match.Value; // 原始"Data Source=..."部分
            var restConnStr = oldconnstr.Replace(originalDataSource, ""); // 保留其他参数（如;Version=3;）

            // 优先从path中判断实际使用的分隔符，更准确
            char? separator = null;
            if (path.Contains("/")) separator = '/';
            else if (path.Contains("\\")) separator = '\\';
            // 路径中没有分隔符时，用原始匹配中可能存在的分隔符
            else if (originalDataSource.Contains("/")) separator = '/';
            else if (originalDataSource.Contains("\\")) separator = '\\';

            // 处理路径为空/仅当前目录/仅上级目录的情况
            if (string.IsNullOrEmpty(path) || path == "." || path == "..")
            {
                // 保留原始路径格式（如"./new.db"或"../new.db"）
                var prefix = string.IsNullOrEmpty(path) ? "" :
                    path == "." ? "./" : "../";
                connstr = $"Data Source={prefix}{replaceConnName}.db{restConnStr}";
            }
            else
            {
                // 处理路径已以分隔符结尾的情况，避免重复添加
                var lastChar = path[path.Length - 1];
                var needAddSeparator = separator.HasValue &&
                                       lastChar != '/' && lastChar != '\\';

                var newPath = needAddSeparator
                    ? $"{path}{separator}{replaceConnName}.db"
                    : $"{path}{replaceConnName}.db";

                connstr = $"Data Source={newPath}{restConnStr}";
            }

            return true;
        }

        // MySQL/PostgreSQL
        if (oldconnstr.Contains("Database=", StringComparison.OrdinalIgnoreCase))
        {
            var regex = new Regex(@"Database=([^;]+)", RegexOptions.IgnoreCase);
            connstr = regex.Replace(oldconnstr, $"Database={replaceConnName}");
            return true;
        }

        // SQL Server
        if (oldconnstr.Contains("Initial Catalog=", StringComparison.OrdinalIgnoreCase))
        {
            var regex = new Regex(@"Initial Catalog=([^;]+)", RegexOptions.IgnoreCase);
            connstr = regex.Replace(oldconnstr, $"Initial Catalog={replaceConnName}");
            return true;
        }

        // Oracle
        if (oldconnstr.Contains("Data Source=Tcp:", StringComparison.OrdinalIgnoreCase) &&
            oldconnstr.Contains("ORCL", StringComparison.OrdinalIgnoreCase))
        {
            // 简化处理Oracle连接字符串
            connstr = oldconnstr.Replace("ORCL", replaceConnName);
            return true;
        }

        return false;
    }
    
    /// <summary>递归解析表达式，收集租户ID</summary>
    private void CollectTenantIds(Expression expression, HashSet<Int32> tenantIds)
    {
        // 处理逻辑表达式（And/Or）
        if (expression is WhereExpression whereExp)
        {
            foreach (var child in whereExp)
                CollectTenantIds(child, tenantIds);
        }
        // 处理字段条件（TenantId = 1 或 TenantId in (1,2)）
        else if (expression is FieldExpression fieldExp)
        {
            // 验证是否为租户字段
            if (fieldExp.Field?.Name != TenantFieldName) return;

            // 处理等于条件（=、==）
            if (new[] { "=", "==" }.Contains(fieldExp.Action, StringComparer.OrdinalIgnoreCase))
            {
                if (int.TryParse(fieldExp.Value?.ToString(), out var id) && id > 0)
                    tenantIds.Add(id);
            }
            // 处理In条件
            else if (fieldExp.Action.Equals("in", StringComparison.OrdinalIgnoreCase) &&
                     fieldExp.Value is IEnumerable values)
            {
                foreach (var val in values)
                {
                    if (int.TryParse(val?.ToString(), out var id) && id > 0)
                        tenantIds.Add(id);
                }
            }
        }
    }
    
    /// <summary>
    /// 生成分片模型
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <returns></returns>
    private ShardModel GetShardModel(int tenantId)
    {
        var tenant = Tenant.FindById(tenantId);
        if (tenant == null || (tenant.DatabaseName.IsNullOrEmpty() && tenant.TableName.IsNullOrEmpty()))
        {
            return null;
        }

        // 生成分片模型（库名+表名）
        var connName = DefaultConnName;
        if (!tenant.DatabaseName.IsNullOrEmpty())
        {
            connName = tenant.DatabaseName.EnsureEnd("_") + DefaultConnName;
            if (!DAL.ConnStrs.ContainsKey(connName))
            {
                if (!GetConnStr(Factory.Table.ConnName, tenant.DatabaseName.EnsureEnd("_") + DefaultConnName, out var connstr))
                    return null;
                DAL.AddConnStr(connName, connstr, null, null);
            }
        }

        var tableName = tenant.TableName.IsNullOrEmpty()
            ? DefaultTable
            : tenant.TableName.EnsureEnd("_") + DefaultTable;

        return new ShardModel(connName, tableName);
    }

    /// <summary>从值中提取租户ID（支持实体对象或直接传入ID）</summary>
    private Int32 GetTenantIdFromValue(Object value)
    {
        // 实体对象：优先从ITenantSource接口获取，其次反射字段
        if (value is IModel entity)
        {
            if (entity is ITenantSource tenantSource)
                return tenantSource.TenantId;

            return entity[Field].ToInt();
        }
        // 直接传入租户ID（Int32）
        else if (value is Int32 id)
        {
            return id;
        }

        return 0;
    }

    #endregion
}