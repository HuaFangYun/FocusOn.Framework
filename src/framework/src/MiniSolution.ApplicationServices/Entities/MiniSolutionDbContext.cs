
using Microsoft.EntityFrameworkCore;

namespace MiniSolution.ApplicationServices.Entities
{
    public class MiniSolutionDbContext<TContext> : DbContext where TContext : DbContext
    {
        public MiniSolutionDbContext(DbContextOptions<TContext> options) : base(options)
        {
        }
    }
}
