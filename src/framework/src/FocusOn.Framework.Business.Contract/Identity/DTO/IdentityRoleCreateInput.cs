using System.ComponentModel.DataAnnotations;

using FocusOn.Framework.Business.Contract.Localizations;

namespace FocusOn.Framework.Business.Contract.Identity.DTO;
/// <summary>
/// 角色创建输入模型。
/// </summary>
public class IdentityRoleCreateInput
{
    /// <summary>
    /// 角色名称。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_Role_Name))]
    [Required(ErrorMessageResourceType = typeof(Locale), ErrorMessageResourceName = nameof(Locale.Message_Validation_FieldIsRequired))]
    public string Name { get; set; }
}
