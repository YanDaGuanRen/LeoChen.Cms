using System.ComponentModel;
using NewLife;
using NewLife.Cube;

namespace LeoChen.Cms.Areas.BasicContent;

[DisplayName("基础内容")]
public class BasicContentArea : AreaBase
{
    public BasicContentArea() : base(nameof(BasicContentArea).TrimEnd("Area")) { }
}