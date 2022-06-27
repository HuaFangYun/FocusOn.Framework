using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FocusOn.Identity.Business.Services.UserManagement.Entities;

namespace FocusOn.Identity.Test.WebApiHost.Entities.Configurations
{
    public class TestUserConfiguration: IdentityUserConfiguration<TestUser,Guid>
    {
        public override void Configure(EntityTypeBuilder<TestUser> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.BirthDay);
        }
    }
}
