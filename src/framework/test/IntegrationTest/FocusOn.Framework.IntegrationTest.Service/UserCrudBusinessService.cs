using Microsoft.Extensions.Logging;
using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.Http;
using FocusOn.Framework.IntegrationTest.Contract;
using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Business.Services.Identity.Services;

namespace FocusOn.Framework.IntegrationTest.Service;
public class UserCrudBusinessService : IdentityUserCrudBusinessService<IdentityDbContext, User, Guid, IdentityUserDetailOutput<Guid>, IdentityUserListOutput, IdentityUserListSearchInput, IdentityUserCreateInput>, IUserCrudBusinessService, IRemotingService
{
    public UserCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public Task<Return<string>> AuthorizeAsync()
    {
        var name = CurrentUser?.Identity?.Name;

        return Task.FromResult(Return<string>.Success(name));
    }

    public Task<Return> SignInAsync([Header] string token)
    {
        Logger.LogInformation(token);
        return Task.FromResult(Return.Success());
    }
}
