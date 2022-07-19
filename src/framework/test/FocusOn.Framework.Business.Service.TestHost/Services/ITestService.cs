using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Services;
using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.Business.Service.TestHost.Services
{
    [Route("api/test")]
    public interface ITestService : ICrudBusinessService<int, TestEntity>
    {

    }

    public class TestService : CrudBusinessService<TestDbContext, TestEntity, int>, ITestService
    {
        public TestService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }

    public class TestDbContext : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("db");
        }
    }
}
