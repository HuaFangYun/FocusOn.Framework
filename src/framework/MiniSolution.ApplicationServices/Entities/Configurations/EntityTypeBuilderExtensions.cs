
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MiniSolution.Core.MuiltyTenancy;

namespace MiniSolution.ApplicationServices.Entities.Configurations;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureKey<TEntity,TKey>(this EntityTypeBuilder<TEntity> builder) where TEntity : EntityBase<TKey>
    {
        builder.HasKey(m => m.Id);
        return builder;
    }

    public static EntityTypeBuilder<TEntity> ConfigureMultiTenancy<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity :class, IHasMultiTenancy
    {
        builder.Property(m => m.TenantId);
        return builder;
    }
}
