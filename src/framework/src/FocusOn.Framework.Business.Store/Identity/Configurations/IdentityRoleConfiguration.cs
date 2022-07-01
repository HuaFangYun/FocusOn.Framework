using FocusOn.Framework.Business.Store.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOn.Framework.Business.Store.Identity.Configurations;

/// <summary>
/// 表示对 <see cref="IdentityRole{TKey}"/> 实体的配置。
/// </summary>
/// <typeparam name="TRole">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public class IdentityRoleConfiguration<TRole, TKey> : IEntityTypeConfiguration<TRole>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleConfiguration{TRole, TKey}"/> 类的新实例。
    /// </summary>
    public IdentityRoleConfiguration()
    {
    }

    /// <summary>
    /// 重写配置实体和数据库字段的映射关系。
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public virtual void Configure(EntityTypeBuilder<TRole> builder)
    {
        builder.ConfigureKey<TRole, TKey>();
        builder.TryConfigureMultiTenancy();
        builder.ToTable(TableName);

        ConfigureUserName(builder);
    }
    /// <summary>
    /// 配置角色名的数据库字段。
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    protected virtual void ConfigureUserName(EntityTypeBuilder<TRole> builder)
    {
        builder.Property(m => m.Name).IsRequired().HasMaxLength(30);
        builder.HasIndex(m => m.Name).IsUnique();
    }
    /// <summary>
    /// 获取数据库表的名称。
    /// </summary>
    protected virtual string TableName => "IdentityRoles";
}
