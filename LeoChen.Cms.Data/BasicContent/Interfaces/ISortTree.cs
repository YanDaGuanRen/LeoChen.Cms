using XCode;

namespace LeoChen.Cms.Data;

/// <summary>
/// 支持排序的树形结构接口
/// </summary>
/// <typeparam name="T">实现该接口的具体类型</typeparam>
public interface ICmsTree<T>
{
    /// <summary>
    /// 子节点集合
    /// </summary>
    static IList<T> Children { get; }

    static extern IList<T> BuildTree(IList<T> flatList = null, int parentId = 0);
}