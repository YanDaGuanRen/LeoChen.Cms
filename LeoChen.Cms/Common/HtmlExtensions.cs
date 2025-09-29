using LeoChen.Cms.Data;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewLife.Cube.Common;

namespace LeoChen.Cms;

/// <summary>Html扩展</summary>
public static class HtmlExtensions
{
    public static IHtmlContent ForSortTreeEditor(IHtmlHelper Html,CmsContent_Sort entity)
    {
        CmsContent_Sort.FindAllByAreaID(CmsAreaContext.CurrentId);
        // var root = entity.GetType().GetValue("Root") as IEntityTree;
        // // 找到完整菜单树，但是排除当前节点这个分支
        // var list = root.FindAllChildsExcept(entity);
        // var data = new SelectList(list, "ID", "Name", entity.Name);
        // return Html.DropDownList(field.Name, data, new { @class = "multiselect" });
        return null;
    }
}