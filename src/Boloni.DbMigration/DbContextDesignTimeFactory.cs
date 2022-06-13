using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Boloni.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Boloni.DbMigration
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<BoloniDbContext>
    {
        public BoloniDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<BoloniDbContext>();
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Boloni;Trusted_Connection=true");
            return new BoloniDbContext(options.Options);
        }
    }
}
