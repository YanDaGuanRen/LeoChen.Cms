using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Filters;
using NewLife.Common;
using XCode.Membership;

namespace LeoChen.Cms.Areas.GlobalConfiguration.Controllers;

/// <summary>系统设置控制器</summary>
[DisplayName("程序设置")]
[GlobalConfigurationArea]
[Menu(60, true)]
public class CmsSetController : ConfigController<CmsSetting>
{

}