using Boloni.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Boloni.Data;
public class BoloniDbContext : DbContext
{
    public BoloniDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new UserConfiguration())
            .ApplyConfiguration(new RoleConfiguration())
            ;
    }
}
