
using Microsoft.EntityFrameworkCore;

using FocusOn.Identity.Business.Services.UserManagement.Entities;

namespace FocusOn.Identity.Business.Services.UserManagement
{
    public interface IUserManagementDbContext<TContext, TUser, TKey> where TContext : DbContext
         where TUser : User<TKey>
    {
        public DbSet<TUser> Users { get; set; }
    }
}
