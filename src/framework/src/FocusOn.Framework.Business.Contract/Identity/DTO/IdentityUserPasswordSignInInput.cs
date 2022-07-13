using System.ComponentModel.DataAnnotations;

using FocusOn.Framework.Business.Contract.Localizations;

namespace FocusOn.Framework.Business.Contract.Identity.DTO;

/// <summary>
/// 用户密码登录的输入。
/// </summary>
public class IdentityUserPasswordSignInInput
{
    /// <summary>
    /// 获取或设置用户名。
    /// </summary>
    [Display(ResourceType =typeof(Locale), Name =nameof(Locale.Field_Identity_User_UserName))]
    [Required(ErrorMessageResourceType =typeof(Locale),ErrorMessageResourceName =nameof(Locale.Message_Validation_FieldIsRequired))]
    public string UserName { get; set; }

    /// <summary>
    /// 获取或设置密码。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_User_Password))]
    [Required(ErrorMessageResourceType = typeof(Locale), ErrorMessageResourceName = nameof(Locale.Message_Validation_FieldIsRequired))]
    public string Password { get; set; }
}
