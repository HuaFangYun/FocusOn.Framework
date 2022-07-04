using FocusOn.Framework.Business.Contract.Localizations;

using System.ComponentModel.DataAnnotations;

namespace FocusOn.Framework.Business.Contract.Identity.DTO;

/// <summary>
/// 角色更新的输入模型。
/// </summary>
public class IdentityRoleUpdateInput
{
    /// <summary>
    /// 角色名称。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_Role_Name))]
    [Required(ErrorMessageResourceType = typeof(Locale), ErrorMessageResourceName = nameof(Locale.Message_Validation_FieldIsRequired))]
    public string Name { get; set; }
}
