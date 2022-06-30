using Microsoft.EntityFrameworkCore;

using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
