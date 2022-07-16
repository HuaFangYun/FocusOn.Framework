using FocusOn.Framework.Endpoint.HttpApi.Identity.Controllers;
using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.Controllers;
public class UserController : IdentityUserCrudApiController<TestDbContext, User, Guid, User, User, User, UserCreateInput, User>, ITestUserBusinessService
{
    public UserController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override IQueryable<User> CreateQuery(User? model)
    {
        if (model is null)
        {
            return QueryWithIdentityResolution;
        }

        return QueryWithIdentityResolution
            .WhereIf(!model.UserName.IsNullOrEmpty(), m => m.UserName.Contains(model.UserName));
    }
}
