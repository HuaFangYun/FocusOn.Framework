
using Boloni.Data.Entities.Abstractions;

namespace Boloni.Data.Entities;

public class Permission:EntityBase,IHasMultiTenancy
{
    /// <summary>
    /// 权限提供者名称，比如 User,Role 等
    /// </summary>
    public string ProviderName { get; set; }
    /// <summary>
    /// 权限的主体，和 <see cref="ProviderName"/> 对应，如果是 Role，则是角色名称，如果是 User，则是用户名
    /// </summary>
    public string ProviderKey { get; set; }
    /// <summary>
    /// 权限名称，一般是策略名称。
    /// </summary>
    public string PermissionName { get; set; }
    public Guid? TenantId { get; set; }
}

public class PermissionProviders
{
    public static readonly string UserPermissionProvider = "User";
    public static readonly string RolePermissionProvider = "Role";
}
