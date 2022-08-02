using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOn.Framework.Business.DbStore.Identity.Configurations;

/// <summary>
/// 表示 <see cref="IdentityUserRole{TKey}"/> 实体的数据库配置。
/// </summary>
/// <typeparam name="TUserRole"><see cref="IdentityUserRole{TKey}"/> 的类型。</typeparam>
/// <typeparam name="TKey">用户和角色的主键类型。</typeparam>
public class IdentityUserRoleConfiguration<TUserRole, TKey> : IEntityTypeConfiguration<TUserRole>
    where TUserRole : IdentityUserRole<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserRoleConfiguration{TUserRole, TKey}"/> 类的新实例。
    /// </summary>
    public IdentityUserRoleConfiguration()
    {
    }

    /// <summary>
    /// 重写配置数据库字段。
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public virtual void Configure(EntityTypeBuilder<TUserRole> builder)
    {
        builder.ToTable("IdentityUserRoles");
    }
}
