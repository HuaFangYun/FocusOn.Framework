using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boloni.Data.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ConfigureId();
        builder.ConfigureTenant();
        builder.Property(m => m.Id).ValueGeneratedOnAdd();
        builder.Property(m => m.Name).HasMaxLength(30);
        builder.Property(m => m.UserName).HasMaxLength(60);
        builder.Property(m => m.HashedPassword).HasMaxLength(128);
        builder.Property(m => m.Email).HasMaxLength(256);
        builder.Property(m => m.Mobile).HasMaxLength(25);

        builder.HasMany(m => m.Roles).WithMany(m => m.Users).UsingEntity(builder =>
        {
            builder.HasKey("UserId", "RoleId");
            builder.ToTable("UserRoles");
        });
        builder.ToTable("Users");
    }
}
