
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MiniSolution.Core.MuiltyTenancy;

namespace MiniSolution.Business.Services.Entities.Configurations;

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
    public static EntityTypeBuilder<TEntity> ConfigureKey<TEntity,TKey>(this EntityTypeBuilder<TEntity> builder) where TEntity : EntityBase<TKey>
    {
        builder.HasKey(m => m.Id);
        return builder;
    }
    /// <summary>
    /// 配置多租户字段。
    /// </summary>
    /// <typeparam name="TEntity">实体类型。</typeparam>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public static EntityTypeBuilder<TEntity> ConfigureMultiTenancy<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity :class, IHasMultiTenancy
    {
        builder.Property(m => m.TenantId);
        builder.HasQueryFilter(m => m.TenantId.HasValue);
        return builder;
    }
}
