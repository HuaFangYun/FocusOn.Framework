using MiniSolution.Business.Contracts;
using MiniSolution.Identity.Business.Services.UserManagement;
using MiniSolution.Identity.Test.WebApiHost.Entities;

namespace MiniSolution.Identity.Test.WebApiHost.Business.Services
{
    public class TestUserApplicationService : UserApplicationService<TestIdentityDbContext, TestUser, Guid>, ITestUserApplicationService,IRemotingService
    {
        public TestUserApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
