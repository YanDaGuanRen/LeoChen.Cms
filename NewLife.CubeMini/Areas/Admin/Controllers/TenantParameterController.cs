using System.ComponentModel;
using NewLife.Cube.Entity;
using XCode.Membership;

namespace NewLife.Cube.Areas.Admin.Controllers;

/// <summary>字典参数</summary>
[DisplayName("租户字典参数")]
[AdminArea]
[Menu(31, true, Icon = "fa-wrench")]
public class CmsParameterController : EntityController<CmsParameter, CmsParameterModel>
{
    static CmsParameterController()
    {
        LogOnChange = true;

        ListFields.RemoveField("Ex1", "Ex2", "Ex3", "Ex4", "Ex5", "Ex6", "UpdateUserID", "UpdateIP");
        ListFields.RemoveCreateField().RemoveUpdateField();
        
    }
}