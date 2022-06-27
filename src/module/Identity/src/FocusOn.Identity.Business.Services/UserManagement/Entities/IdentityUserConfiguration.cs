
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FocusOn.Business.Services.Entities.Configurations;

namespace FocusOn.Identity.Business.Services.UserManagement.Entities;

public class IdentityUserConfiguration<TUser, TKey> : IEntityTypeConfiguration<TUser> where TUser : IdentityUser<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TUser> builder)
    {
        builder.ConfigureKey<TUser, TKey>();
        builder.ToTable(TableName);
    }

    protected virtual string TableName => "IdentityUsers";
}
