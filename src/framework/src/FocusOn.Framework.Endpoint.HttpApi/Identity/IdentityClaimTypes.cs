namespace FocusOn.Framework.Endpoint.HttpApi.Identity;

/// <summary>
/// Identity 的声明类型。
/// </summary>
public static class IdentityClaimTypes
{
    /// <summary>
    /// 租户 Id。
    /// </summary>
    public const string TenantId = "FocusOn.Framework.Identity.TenantId";
    /// <summary>
    /// 租户名称
    /// </summary>
    public const string TenantName = "FocusOn.Framework.Identity.TenantName";
    /// <summary>
    /// 用户名。
    /// </summary>
    public const string UserName = "FocusOn.Framework.Identity.UserName";
    /// <summary>
    /// 用户 Id。
    /// </summary>
    public const string UserId = "FocusOn.Framework.Identity.UserId";
}
