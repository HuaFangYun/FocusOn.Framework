using Microsoft.EntityFrameworkCore;

using MiniSolution.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace MiniSolution.Endpoint.HttpApi.Test.WebHost.BusinessServices
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
