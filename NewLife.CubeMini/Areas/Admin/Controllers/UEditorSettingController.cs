using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Filters;
using NewLife.Common;
using XCode.Membership;

namespace NewLife.Cube.Areas.Admin.Controllers;

/// <summary>系统设置控制器</summary>
[DisplayName("UEditorSetting设置")]
[AdminArea]
[Menu(0, true)]
public class UEditorSetController : ConfigController<UEditorSetting>
{

}