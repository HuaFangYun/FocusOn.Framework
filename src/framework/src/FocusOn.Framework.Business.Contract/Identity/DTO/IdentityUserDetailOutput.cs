﻿using System.ComponentModel.DataAnnotations;

using FocusOn.Framework.Business.Contract.Localizations;

namespace FocusOn.Framework.Business.Contract.Identity.DTO;

/// <summary>
/// 用户详情输出模型。
/// </summary>
public class IdentityUserDetailOutput
{
    /// <summary>
    /// 用户名。
    /// </summary>
    [Display(ResourceType = typeof(Locale), Name = nameof(Locale.Field_Identity_User_UserName))]
    public string? UserName { get; set; }

    /// <summary>
    /// 获取或设置用户所属的角色名称集合。
    /// </summary>
    public IEnumerable<string>? Roles { get; set; }
}
