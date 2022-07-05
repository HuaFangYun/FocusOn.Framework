using System.ComponentModel.DataAnnotations;

using FocusOn.Framework.Business.Contract.Localizations;

namespace FocusOn.Framework.Business.Contract.Identity.DTO;
/// <summary>
/// 用户列表查询条件输入。
/// </summary>
public class IdentityUserListSearchInput
{
    /// <summary>
    /// 用户名。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_User_UserName))]
    public virtual string? UserName { get; set; }
}
