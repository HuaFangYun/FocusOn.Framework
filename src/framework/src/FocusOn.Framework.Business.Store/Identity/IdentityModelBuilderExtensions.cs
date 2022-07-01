using FocusOn.Framework.Business.Store.Identity.Configurations;

using Microsoft.EntityFrameworkCore;

namespace FocusOn.Framework.Business.Store.Identity;

/// <summary>
/// Identity 的 <see cref="ModelBuilder"/> 扩展。
/// </summary>
public static class IdentityModelBuilderExtensions
{
    /// <summary>
    /// 应用 Identity 的数据库映射配置。
    /// </summary>
    /// <typeparam name="TUserConfiguration">用户配置类型。</typeparam>
    /// <typeparam name="TRoleConfiguration">角色配置类型。</typeparam>
    /// <typeparam name="TUsrRoleConfiguration">用户角色关联配置类型。</typeparam>
    /// <typeparam name="TUser">用户类型。</typeparam>
    /// <typeparam name="TRole">角色类型。</typeparam>
    /// <typeparam name="TUserRole">用户角色关联类型。</typeparam>
    /// <typeparam name="TKey">主键类型。</typeparam>
    /// <param name="modelBuilder"><see cref="ModelBuilder"/> 实例。</param>
    public static ModelBuilder ApplyIdentityConfiguration<TUserConfiguration, TRoleConfiguration, TUsrRoleConfiguration, TUser, TRole, TUserRole, TKey>(this ModelBuilder modelBuilder)
        where TUserConfiguration : IdentityUserConfiguration<TUser, TKey>, new()
        where TRoleConfiguration : IdentityRoleConfiguration<TRole, TKey>, new()
        where TUsrRoleConfiguration : IdentityUserRoleConfiguration<TUserRole, TKey>, new()
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        modelBuilder.ApplyConfiguration(new TUserConfiguration())
            .ApplyConfiguration(new TRoleConfiguration())
            .ApplyConfiguration(new TUsrRoleConfiguration())
            ;

        modelBuilder.Entity<TUser>()
            .HasMany<TUserRole>().WithOne().HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TRole>().HasMany<TUserRole>().WithOne().HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Cascade);

        return modelBuilder;
    }
    /// <summary>
    /// 应用 Identity 的数据库映射配置。
    /// </summary>
    /// <typeparam name="TUserConfiguration">用户配置类型。</typeparam>
    /// <typeparam name="TRoleConfiguration">角色配置类型。</typeparam>
    /// <typeparam name="TUser">用户类型。</typeparam>
    /// <typeparam name="TRole">角色类型。</typeparam>
    /// <typeparam name="TKey">主键类型。</typeparam>
    /// <param name="modelBuilder"><see cref="ModelBuilder"/> 实例。</param>
    public static ModelBuilder ApplyIdentityConfiguration<TUserConfiguration, TRoleConfiguration, TUser, TRole, TKey>(this ModelBuilder modelBuilder)
        where TUserConfiguration : IdentityUserConfiguration<TUser, TKey>, new()
        where TRoleConfiguration : IdentityRoleConfiguration<TRole, TKey>, new()
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        => modelBuilder.ApplyIdentityConfiguration<TUserConfiguration, TRoleConfiguration, IdentityUserRoleConfiguration<IdentityUserRole<TKey>, TKey>, TUser, TRole, IdentityUserRole<TKey>, TKey>();
}
