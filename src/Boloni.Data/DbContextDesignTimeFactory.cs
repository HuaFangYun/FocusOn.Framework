
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Boloni.Data;

public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<BoloniDbContext>
{
    public BoloniDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<BoloniDbContext>();
        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Boloni;Trusted_Connection=true");
        return new BoloniDbContext(options.Options);
    }
}
