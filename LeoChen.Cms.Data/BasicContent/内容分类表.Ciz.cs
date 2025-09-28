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

public partial class CmsContent_Sort:ICmsTree<CmsContent_Sort>
{
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public IList<CmsContent_Sort> Children => Extends.Get(nameof(Children), e => FindChilds())!;
    protected IList<CmsContent_Sort> FindChilds() => FindAllByAreaIDAndPid(CmsAreaContext.CurrentId,ID)!;
    
    
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public CmsContent_Sort Parent => Extends.Get(nameof(Parent), e => FindParent())!;
    protected CmsContent_Sort FindParent() => FindByID(ID)!;
    
    
    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public IList<CmsContent_Sort> MyChildren { get; set; }

    [XmlIgnore, IgnoreDataMember, ScriptIgnore]
    public IList<int> AllChildrenId { get; set; }
    public static IList<CmsContent_Sort> GetTree()
    { 
        var areaid = CmsAreaContext.CurrentId;
        return FindAllByAreaIDAndPid(areaid, 0);
        // var list = FindAllByAreaID(areaid);//取出指定区域的所有菜单列表
        // return BuildTree1(list, 0);
    }
    
    // public static List<CmsContent_Sort> BuildTree1(IList<CmsContent_Sort> flatList, int parentId = 0)
    // {
    //     return flatList
    //         .Where(c => c.Pid == parentId)
    //         .Select(d =>
    //             {
    //                 var cmsContentSortTree = new CmsContent_SortTree();
    //                 NewLife.Reflection.Reflect.Copy(cmsContentSortTree, d);
    //                 cmsContentSortTree.Children = BuildTree(flatList, d.ID);
    //                 return cmsContentSortTree;
    //             }
    //             )
    //         .OrderBy(b => b.Sorting)
    //         .ToList();
    // }

    


}
