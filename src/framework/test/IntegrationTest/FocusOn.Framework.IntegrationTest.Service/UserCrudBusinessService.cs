using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.Http;
using FocusOn.Framework.IntegrationTest.Contract;
using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Business.Service.Identity.Services;

namespace FocusOn.Framework.IntegrationTest.Service;
public class UserCrudBusinessService : IdentityUserCrudBusinessService<IdentityDbContext, User, Guid, IdentityUserDetailOutput<Guid>, IdentityUserListOutput, IdentityUserListSearchInput, IdentityUserCreateInput>, IUserCrudBusinessService
{
    private readonly IRoleCrudBusinessService _roleCrudBusinessService;

    public UserCrudBusinessService(IServiceProvider serviceProvider, IRoleCrudBusinessService roleCrudBusinessService) : base(serviceProvider)
    {
        this._roleCrudBusinessService = roleCrudBusinessService;
    }

    [Authorize]
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
