using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boloni.Data.Configurations;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ConfigureId();
        builder.ConfigureTenant();
        builder.Property(m => m.Name).HasMaxLength(30).IsRequired();

        builder.ToTable("Roles");
    }
}
