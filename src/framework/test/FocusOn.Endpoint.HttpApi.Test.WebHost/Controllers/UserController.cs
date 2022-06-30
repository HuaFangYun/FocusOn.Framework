using FocusOn.Framework.Business.Contract.DTO;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;
using FocusOn.Framework.Endpoint.HttpApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.Controllers;

public class UserController : CrudApiControllerBase<TestDbContext, User, Guid>, ITestUserBusinessService
{
    [HttpGet("by-name/{name}")]
    public async Task<OutputResult<User?>> GetByNameAsync(string name)
    {
        var entity = await Query.SingleOrDefaultAsync(m => m.Name.Equals(name), CancellationToken);
        if (entity is null)
        {
            return OutputResult<User?>.Failed("User not found");
        }
        var result = MapToModel(entity);
        return OutputResult<User?>.Success(result);
    }
}
