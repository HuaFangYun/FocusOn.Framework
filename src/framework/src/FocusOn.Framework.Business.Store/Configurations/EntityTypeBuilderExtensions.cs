﻿using FocusOn.Framework.Modules;
using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.MuiltyTenancy;
using FocusOn.Framework.Business.DbStore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOn.Framework.Business.DbStore.Configurations;

/// <summary>
/// <see cref="EntityTypeBuilder{TEntity}"/> 的扩展。
/// </summary>
public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// 配置数据库字段的兼容。
    /// </summary>
    /// <typeparam name="TEntity">实体。</typeparam>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public static EntityTypeBuilder<TEntity> ConfigureConvensions<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        => builder.TryConfigureMultiTenancy()
                 .TryConfigureCreateTime();

    /// <summary>
    /// 配置 Id 作为主键。
    /// </summary>
    /// <typeparam name="TEntity">继承 <see cref="EntityBase{TKey}"/> 类的实体类型。</typeparam>
    /// <typeparam name="TKey">主键类型。</typeparam>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public static EntityTypeBuilder<TEntity> ConfigureKey<TEntity, TKey>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedOnAdd();
        return builder;
    }

    /// <summary>
    /// 尝试配置多租户字段。
    /// </summary>
    /// <typeparam name="TEntity">实体类型。</typeparam>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public static EntityTypeBuilder<TEntity> TryConfigureMultiTenancy<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        if (builder.Metadata is IHasMultiTenancy multiTenancy)
        {
            builder.Property(nameof(IHasMultiTenancy.TenantId));
            builder.HasQueryFilter(m => EF.Property<Guid>(m, nameof(IHasMultiTenancy.TenantId)) == multiTenancy.TenantId);
        }
        return builder;
    }

    /// <summary>
    /// 尝试配置创建时间字段。
    /// </summary>
    /// <typeparam name="TEntity">实体类型。</typeparam>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> 实例。</param>
    public static EntityTypeBuilder<TEntity> TryConfigureCreateTime<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        if (builder.Metadata is IHasCreateTime createTime)
        {
            builder.Property(nameof(IHasCreateTime.CreateTime));
        }
        return builder;
    }
}
