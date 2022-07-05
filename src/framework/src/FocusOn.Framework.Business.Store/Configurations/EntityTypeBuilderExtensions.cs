using FocusOn.Framework.MuiltyTenancy;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOn.Framework.Business.Store.Configurations;

/// <summary>
/// <see cref="EntityTypeBuilder{TEntity}"/> 的扩展。
/// </summary>
public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// 配置 Id 作为主键。
    /// </summary>
    /// <typeparam name="TEntity">继承 <see cref="EntityBase{TKey}"/> 类的实体类型。</typeparam>
    /// <typeparam name="TKey">主键类型。</typeparam>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public static EntityTypeBuilder<TEntity> ConfigureKey<TEntity, TKey>(this EntityTypeBuilder<TEntity> builder) where TEntity : EntityBase<TKey>
    {
        builder.HasKey(m => m.Id);
        return builder;
    }
    /// <summary>
    /// 配置多租户字段。
    /// </summary>
    /// <typeparam name="TEntity">实体类型。</typeparam>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public static EntityTypeBuilder<TEntity> ConfigureMultiTenancy<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IHasMultiTenancy
    {
        builder.TryConfigureMultiTenancy<TEntity>();
        return builder;
    }

    public static bool TryConfigureMultiTenancy<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        if (builder.Metadata is IHasMultiTenancy multiTenancy)
        {
            builder.Property(nameof(IHasMultiTenancy.TenantId));
            builder.HasQueryFilter(m => EF.Property<Guid>(m, nameof(IHasMultiTenancy.TenantId)) == multiTenancy.TenantId);
            return true;
        }
        return false;
    }
}
