using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using FocusOn.Framework.Business.Service;
using FocusOn.Framework.IntegrationTest.Contract;
using FocusOn.Framework.Business.Service.Identity.Services;

namespace FocusOn.Framework.IntegrationTest.Service
{
    public class AccountService : IdentityUserCrudBusinessService<IdentityDbContext, User, Guid>, IAccountService
    {
        public AccountService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<Return> SignInAsync(string account, string password)
        {
            return Return.Success().ToResultTask();
        }
    }
}
