using Microsoft.EntityFrameworkCore;

using FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
