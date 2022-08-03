using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.Http;
using FocusOn.Framework.Business.Contract.Identity;
using FocusOn.Framework.Business.Contract.Identity.DTO;

namespace FocusOn.Framework.IntegrationTest.Contract
{
    [Route("api/roles")]
    public interface IRoleCrudBusinessService : IIdentityRoleCrudBusinessService<Guid, IdentityRoleDetailOutput, IdentityRoleListOutput, IdentityRoleListSearchInput, IdentityRoleCreateInput>, IRemotingService
    {
    }
}
