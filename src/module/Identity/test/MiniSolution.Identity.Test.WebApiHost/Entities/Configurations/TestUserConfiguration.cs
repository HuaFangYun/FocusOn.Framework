using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MiniSolution.Identity.Business.Services.UserManagement.Entities;

namespace MiniSolution.Identity.Test.WebApiHost.Entities.Configurations
{
    public class TestUserConfiguration:UserConfiguration<TestUser,Guid>
    {
        public override void Configure(EntityTypeBuilder<TestUser> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.BirthDay);
        }
    }
}
