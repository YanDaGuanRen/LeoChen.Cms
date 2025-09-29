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
    IList<T>  MyChildren { get; set; }
    /// <summary>子孙实体集合。以深度层次树结构输出</summary>
    IList<int> AllChildsID { get; set; }
    /// <summary>树形节点名，根据深度带全角空格前缀</summary>
    String TreeNodeText { get; }
    IList<T> FindAllChildsExcept(IList<T> flatList = null, int parentId = 0);
}