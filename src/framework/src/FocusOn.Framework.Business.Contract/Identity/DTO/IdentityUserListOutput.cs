using System.ComponentModel.DataAnnotations;

using FocusOn.Framework.Business.Contract.Localizations;

namespace FocusOn.Framework.Business.Contract.Identity.DTO;
/// <summary>
/// 用户列表的输出模型。
/// </summary>
public class IdentityUserListOutput
{
    /// <summary>
    /// 用户名。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_User_UserName))]
    public string UserName { get; set; }
}
