using Boloni.Data.Entities;
using Boloni.Data.Entities.Abstractions;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boloni.Data.Configurations;
public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureTenant<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity :class, IHasMultiTenancy
    {
        builder.Property(m => m.TenantId);
        builder.HasQueryFilter(m => m.TenantId.HasValue);
        return builder;
    }

    public static EntityTypeBuilder<TEntity> ConfigureId<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : EntityBase
    {
        builder.HasKey(m => m.Id);
        return builder;
    }
}
