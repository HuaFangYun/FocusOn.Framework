
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MiniSolution.Business.Services.Entities.Configurations;

namespace MiniSolution.Identity.Business.Services.UserManagement.Entities;

public class UserConfiguration<TUser, TKey> : IEntityTypeConfiguration<TUser> where TUser : User<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TUser> builder)
    {
        builder.ConfigureKey<TUser, TKey>();
        builder.ToTable(TableName);
    }

    protected virtual string TableName => "MiniUsers";
}
