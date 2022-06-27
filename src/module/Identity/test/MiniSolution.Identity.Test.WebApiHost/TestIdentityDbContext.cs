using Microsoft.EntityFrameworkCore;

using FocusOn.Identity.Test.WebApiHost.Entities.Configurations;

namespace FocusOn.Identity.Test.WebApiHost
{
    public class TestIdentityDbContext : DbContext
    {
        public TestIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestUserConfiguration());
        }
    }
}
