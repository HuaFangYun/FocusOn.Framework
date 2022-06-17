
using Microsoft.EntityFrameworkCore;

using MiniSolution.Identity.ApplicationServices.UserManagement.Entities;

namespace MiniSolution.Identity.ApplicationServices
{
    public interface IUserManagementDbContext<TContext,TUser,TKey> where TContext:DbContext
         where TUser:User<TKey>
    {
        public DbSet<TUser> Users { get; set; }
    }
}
