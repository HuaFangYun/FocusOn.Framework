using System.ComponentModel.DataAnnotations;

using FocusOn.Framework.Business.Contract.Localizations;

namespace FocusOn.Framework.Business.Contract.Identity.DTO;

/// <summary>
/// 表示创建用户的输入模型。
/// </summary>
public class IdentityUserCreateInput
{
    /// <summary>
    /// 获取或设置用户名。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_User_UserName))]
    [Required(ErrorMessageResourceType = typeof(Locale), ErrorMessageResourceName = nameof(Locale.Message_Validation_FieldIsRequired))]
    public virtual string UserName { get; set; }
}

/// <summary>
/// 表示使用密码创建用户的输入模型。
/// </summary>
public class IdentityUserPasswordCreateInput : IdentityUserCreateInput
{
    /// <summary>
    /// 获取或设置密码。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_User_Password))]
    [Required(ErrorMessageResourceType = typeof(Locale), ErrorMessageResourceName = nameof(Locale.Message_Validation_FieldIsRequired))]
    public virtual string Password { get; set; }
}