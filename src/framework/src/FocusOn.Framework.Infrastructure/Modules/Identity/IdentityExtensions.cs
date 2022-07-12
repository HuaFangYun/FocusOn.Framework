using System.Security.Claims;
using System.Security.Principal;

namespace FocusOn.Framework.Modules.Identity;

/// <summary>
/// Identity 扩展。
/// </summary>
public static class IdentityExtensions
{
    /// <summary>
    /// 转换成 <see cref="ClaimsIdentity"/> 类型。
    /// </summary>
    /// <param name="identity"></param>
    /// <returns></returns>
    public static ClaimsIdentity? ToClaimIdentity(this IIdentity identity)
        => identity as ClaimsIdentity;
    /// <summary>
    /// 获取第一个声明类型的值。
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="type">声明类型。</param>
    /// <returns>声明类型的值。</returns>
    public static string? FindFirstClaimValue(this IIdentity identity, string type)
        => identity.ToClaimIdentity()?.FindFirst(type)?.Value;

    /// <summary>
    /// 获取声明类型是 <see cref="IdentityClaimTypes.TenantId"/> 的租户 Id。
    /// </summary>
    public static Guid? GetTenantId(this IIdentity identity)
    {
        var value = identity.FindFirstClaimValue(IdentityClaimTypes.TenantId);
        if (Guid.TryParse(value, out var id))
        {
            return id;
        }
        return default;
    }
    /// <summary>
    /// 获取声明类型是 <see cref="IdentityClaimTypes.UserName"/> 的用户 Id。
    /// </summary>
    public static string? GetUserId(this IIdentity identity) => identity.FindFirstClaimValue(IdentityClaimTypes.UserId);

    /// <summary>
    /// 获取声明类型是 <see cref="IdentityClaimTypes.TenantName"/> 的租户名称。
    /// </summary>
    public static string? GetTenantName(this IIdentity identity) => identity.FindFirstClaimValue(IdentityClaimTypes.TenantName);
}
