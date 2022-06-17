using Microsoft.EntityFrameworkCore;

using MiniSolution.Identity.Test.WebApiHost.Entities.Configurations;

namespace MiniSolution.Identity.Test.WebApiHost
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
