using FocusOn.Framework.Business.Store.Identity;
using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices.Entities;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

using Microsoft.EntityFrameworkCore;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyIdentityConfiguration<UserConfiguration, RoleConfiguration, User, Role, Guid>();
        }
    }
}
