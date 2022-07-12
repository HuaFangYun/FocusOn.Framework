using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;
public class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
{
    public TestDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TestDbContext>();
        builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FocusOn;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new TestDbContext(builder.Options);
    }
}
