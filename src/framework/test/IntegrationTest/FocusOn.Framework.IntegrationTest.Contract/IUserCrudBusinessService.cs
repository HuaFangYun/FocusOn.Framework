using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.Http;
using FocusOn.Framework.Business.Contract.Identity;
using FocusOn.Framework.Business.Contract.Identity.DTO;

namespace FocusOn.Framework.IntegrationTest.Contract;
[Route("api/user")]
public interface IUserCrudBusinessService : IIdentityUserCrudBusinessService<Guid, IdentityUserDetailOutput<Guid>, IdentityUserListOutput, IdentityUserListSearchInput, IdentityUserCreateInput>, IRemotingService
{
    [Post("sign-in")]
    Task<Return> SignInAsync([Header] string token);

    [Get("authorize")]
    Task<Return<string>> AuthorizeAsync();
}
