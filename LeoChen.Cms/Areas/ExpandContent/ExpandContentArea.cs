using System.ComponentModel;
using NewLife;
using NewLife.Cube;

namespace LeoChen.Cms.Areas.ExpandContent;

[DisplayName("扩展内容")]
public class ExpandContentArea : AreaBase
{
    public ExpandContentArea() : base(nameof(ExpandContentArea).TrimEnd("Area")) { }
}