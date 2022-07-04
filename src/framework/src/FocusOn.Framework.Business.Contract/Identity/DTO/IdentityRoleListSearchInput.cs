using System.ComponentModel.DataAnnotations;

using FocusOn.Framework.Business.Contract.Localizations;

namespace FocusOn.Framework.Business.Contract.Identity.DTO;
/// <summary>
/// 角色列表查询输入模型。
/// </summary>
public class IdentityRoleListSearchInput
{
    /// <summary>
    /// 角色名称。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_Role_Name))]
    public string? Name { get; set; }
}
