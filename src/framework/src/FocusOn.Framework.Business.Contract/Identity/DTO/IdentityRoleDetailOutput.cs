using System.ComponentModel.DataAnnotations;

using FocusOn.Framework.Business.Contract.Localizations;

namespace FocusOn.Framework.Business.Contract.Identity.DTO;
/// <summary>
/// 角色详情输出模型。
/// </summary>
public class IdentityRoleDetailOutput
{
    /// <summary>
    /// 角色名称。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_Role_Name))]
    public string Name { get; set; }
}
