using System.ComponentModel;
using NewLife;
using NewLife.Cube;

namespace LeoChen.Cms.Areas.GlobalConfiguration;

[DisplayName("全局配置")]
public class GlobalConfigurationArea : AreaBase
{
    public GlobalConfigurationArea() : base(nameof(GlobalConfigurationArea).TrimEnd("Area")) { }
}