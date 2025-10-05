using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Common;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;
using XCode.Shards;

namespace LeoChen.Cms.Data;

public partial class CmsContent_Sort
{
    // [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    // public IList<CmsContent_Sort> Children => Extends.Get(nameof(Children), e => FindChilds())!;
    // protected IList<CmsContent_Sort> FindChilds() => FindAllByAreaIDAndPid(CmsAreaContext.CurrentId,ID)!;
    //
    //
    //
    // [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    // public CmsContent_Sort Parent => Extends.Get(nameof(Parent), e => FindParent())!;
    // protected CmsContent_Sort FindParent() => FindByID(ID)!;
    
    
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public IList<CmsContent_Sort> Children { get; set; }
    
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public String TreeNodeText { get; set; }
    public static IList<CmsContent_Sort> GetTree()
    { 
        var areaid = CmsAreaContext.CurrentId;
        // return FindAllByAreaIDAndPid(areaid, 0);
        var list = FindAllByAreaID(areaid);//取出指定区域的所有菜单列表
        return BuildTree1(list);
    }
    public static IList<CmsContent_Sort> BuildTree1(IList<CmsContent_Sort> flatList, int parentId = 0, HashSet<int> visitedIds = null)
    {
        visitedIds ??= new HashSet<int>();
        return flatList
            .Where(c => c.Pid == parentId)
            .Select(d =>
                {
                    if (visitedIds.Contains(d.ID))return null;
                    visitedIds.Add(d.ID);
                    d.Children = BuildTree1(flatList, d.ID, visitedIds);
                    return d;
                }
                )
            .OrderBy(b => b.Sorting)
            .ToList();
    }
    public static IList<CmsContent_Sort> GetTreeList(int id )
    { 
        var areaid = CmsAreaContext.CurrentId;
        // return FindAllByAreaIDAndPid(areaid, 0);
        var list = FindAllByAreaID(areaid);//取出指定区域的所有菜单列表
        return GetTreeList1(list,id);
    }

        public static IList<CmsContent_Sort> GetTreeList1(IList<CmsContent_Sort> flatList, int id,int parentId = 0, int depth = 1,HashSet<int> visitedIds = null)
        {
            var result = new List<CmsContent_Sort>();
            if (parentId == 0)
            {
                var e = new CmsContent_Sort();
                e.ID = 0;
                e.TreeNodeText = "|- 顶级栏目";
                result.Add(e);
            }
            visitedIds ??= new HashSet<int>();
            var nodes = flatList.Where(c => c.Pid == parentId).OrderBy(b => b.Sorting).ToList();
            foreach (var node in nodes)
            {
                if (node.ID == id) continue;
                // 设置节点的显示文本，包含深度信息
                node.TreeNodeText = new String('　', depth) + "|- " + node.Name;
                if (visitedIds.Contains(node.ID)) continue;
                // 添加当前节点到结果列表
                result.Add(node);
                visitedIds.Add(node.ID);
                // 递归获取子节点
                var children = GetTreeList1(flatList, id,node.ID, depth + 1);
                foreach (var child in children)
                {
                    if (child.ID == id) continue;
                    if (visitedIds.Contains(child.ID)) continue;
                    result.Add(child);
                    visitedIds.Add(child.ID);
                }
            }
            return result;
        }
    

    // public static IList<CmsContent_Sort> GetAllChildren(CmsContent_Sort cmsContentSort)
    // {
    //     
    // }

    public override bool Equals(object obj)
    {
        if (!(obj is CmsContent_Sort cmsContentSort)) return false;
        if (ID == cmsContentSort.ID) return true;
        return false;
    }

    public bool IsContains(CmsContent_Sort cmsContentSort)
    {
        if (Equals(cmsContentSort)) return true;

        if (Children is { Count: > 0 })
        {
            foreach (var item in Children)
            {
                if (item.IsContains(cmsContentSort)) return true;
            }
        }
        return false;
    }
    


    


}
