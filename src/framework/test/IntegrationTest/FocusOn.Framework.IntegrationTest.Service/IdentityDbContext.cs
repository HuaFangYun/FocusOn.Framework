using Microsoft.EntityFrameworkCore;

namespace FocusOn.Framework.IntegrationTest.Service;
public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}
