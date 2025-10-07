using System.ComponentModel;
using NewLife;
using NewLife.Cube;

namespace LeoChen.Cms.Areas.ExpandContent;

[DisplayName("扩展内容")]
[Menu(600, true, Icon = "fa-pencil-square-o")]
public class ExpandContentArea : AreaBase
{
    public ExpandContentArea() : base(nameof(ExpandContentArea).TrimEnd("Area")) { }
}