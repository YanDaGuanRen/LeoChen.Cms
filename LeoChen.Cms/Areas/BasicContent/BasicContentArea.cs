using System.ComponentModel;
using NewLife;
using NewLife.Cube;

namespace LeoChen.Cms.Areas.BasicContent;

[DisplayName("基础内容")]
[Menu(800, true, Icon = "fa-desktop")]
public class BasicContentArea : AreaBase
{
    public BasicContentArea() : base(nameof(BasicContentArea).TrimEnd("Area")) { }
}