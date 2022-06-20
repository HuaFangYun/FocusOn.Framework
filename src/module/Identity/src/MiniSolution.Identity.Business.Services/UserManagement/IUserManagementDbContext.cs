
using Microsoft.EntityFrameworkCore;

using MiniSolution.Identity.Business.Services.UserManagement.Entities;

namespace MiniSolution.Identity.Business.Services.UserManagement
{
    public interface IUserManagementDbContext<TContext, TUser, TKey> where TContext : DbContext
         where TUser : User<TKey>
    {
        public DbSet<TUser> Users { get; set; }
    }
}
