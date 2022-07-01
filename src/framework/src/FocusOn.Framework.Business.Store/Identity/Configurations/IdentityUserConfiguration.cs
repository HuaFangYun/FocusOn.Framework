using FocusOn.Framework.Business.Store.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOn.Framework.Business.Store.Identity.Configurations;

/// <summary>
/// 表示对 <see cref="IdentityUser{TKey}"/> 实体的配置。
/// </summary>
/// <typeparam name="TUser">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public class IdentityUserConfiguration<TUser, TKey> : IEntityTypeConfiguration<TUser>
    where TUser : IdentityUser<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserConfiguration{TUser, TKey}"/> 类的新实例。
    /// </summary>
    public IdentityUserConfiguration()
    {
    }

    /// <summary>
    /// 重写配置实体和数据库字段的映射关系。
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public virtual void Configure(EntityTypeBuilder<TUser> builder)
    {
        builder.ConfigureKey<TUser, TKey>();
        builder.TryConfigureMultiTenancy();
        builder.ToTable(TableName);

        ConfigureUserName(builder);
    }

    /// <summary>
    /// 配置用户名的数据库字段。
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    protected virtual void ConfigureUserName(EntityTypeBuilder<TUser> builder)
    {
        builder.Property(m => m.UserName).IsRequired().HasMaxLength(30);
        builder.HasIndex(m => m.UserName).IsUnique();
    }

    /// <summary>
    /// 获取数据库表的名称。
    /// </summary>
    protected virtual string TableName => "IdentityUsers";
}
