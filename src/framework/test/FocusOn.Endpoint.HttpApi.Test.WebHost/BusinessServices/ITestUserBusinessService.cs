using Microsoft.EntityFrameworkCore;

using FocusOn.Business.Contracts;
using FocusOn.Business.Contracts.DTO;
using FocusOn.Business.Services;
using FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices;

public interface ITestUserBusinessService:ICrudBusinessService<Guid,User>
{
    Task<OutputResult<User>> GetByNameAsync(string name);
}

[RemotingService("users")]
public class TestUserBusinessService : EfCoreCrudBusinessServiceBase<TestDbContext, User, Guid>, ITestUserBusinessService,IRemotingService
{
    public TestUserBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [RemotingService("user",Disabled =true)]
    public async Task<OutputResult<User>> GetByNameAsync(string name)
    {
        var user = await Query.SingleOrDefaultAsync(m => m.Name.Equals(name), CancellationToken);
        return OutputResult<User>.Success(user);
    }
}
