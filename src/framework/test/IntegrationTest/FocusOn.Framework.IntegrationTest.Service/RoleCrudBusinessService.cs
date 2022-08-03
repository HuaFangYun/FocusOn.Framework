using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using FocusOn.Framework.IntegrationTest.Contract;
using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Business.Service.Identity.Services;

namespace FocusOn.Framework.IntegrationTest.Service
{
    public class RoleCrudBusinessService : IdentityRoleCrudBusinessService<IdentityDbContext, Role, Guid, IdentityRoleDetailOutput, IdentityRoleListOutput, IdentityRoleListSearchInput, IdentityRoleCreateInput>, IRoleCrudBusinessService
    {
        public RoleCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
