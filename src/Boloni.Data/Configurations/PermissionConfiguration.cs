
using Boloni.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boloni.Data.Configurations
{
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ConfigureId();
            builder.ConfigureTenant();
            builder.Property(m=>m.ProviderName).IsRequired().HasMaxLength(256);
            builder.Property(m => m.ProviderKey).IsRequired().HasMaxLength(20);
            builder.Property(m=>m.PermissionName).IsRequired().HasMaxLength(256);
        }
    }
}
