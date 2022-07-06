using FocusOn.Framework.Business.Contract.DTO;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;
using FocusOn.Framework.Endpoint.HttpApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;
using FocusOn.Framework.Endpoint.HttpApi.Identity.Controllers;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.Controllers;

public class UserController : IdentityUserCrudApiController<TestDbContext, User, Guid>, ITestUserBusinessService
{
    public UserController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
