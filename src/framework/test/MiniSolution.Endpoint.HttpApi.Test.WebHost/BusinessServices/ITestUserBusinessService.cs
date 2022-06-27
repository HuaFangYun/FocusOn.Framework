using Microsoft.EntityFrameworkCore;

using MiniSolution.Business.Contracts;
using MiniSolution.Business.Contracts.DTO;
using MiniSolution.Business.Services;
using MiniSolution.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace MiniSolution.Endpoint.HttpApi.Test.WebHost.BusinessServices;

public interface ITestUserBusinessService:ICrudBusinessService<Guid,User>
{
    Task<OutputResult<User>> GetByNameAsync(string name);
}

public class TestUserBusinessService : EfCoreCrudApplicationServiceBase<TestDbContext, User, Guid>, ITestUserBusinessService,IRemotingService
{
    public TestUserBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [RemotingService("user")]
    public async Task<OutputResult<User>> GetByNameAsync(string name)
    {
        var user = await Query.SingleOrDefaultAsync(m => m.Name.Equals(name), CancellationToken);
        return OutputResult<User>.Success(user);
    }
}
