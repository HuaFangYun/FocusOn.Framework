using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Business.Services.Identity.Services;
using FocusOn.Framework.IntegrationTest.Contract;

namespace FocusOn.Framework.IntegrationTest.Service;
public class UserCrudBusinessService : IdentityUserCrudBusinessService<IdentityDbContext, User, Guid, IdentityUserDetailOutput, IdentityUserListOutput, IdentityUserListSearchInput, IdentityUserCreateInput>, IUserCrudBusinessService, IRemotingService
{
    public UserCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
