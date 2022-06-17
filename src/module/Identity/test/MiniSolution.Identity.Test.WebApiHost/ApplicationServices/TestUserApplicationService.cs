using MiniSolution.Identity.ApplicationServices.UserManagement;
using MiniSolution.Identity.Test.WebApiHost.Entities;

namespace MiniSolution.Identity.Test.WebApiHost.ApplicationServices
{
    public class TestUserApplicationService : UserApplicationService<TestIdentityDbContext, TestUser, Guid>, ITestUserApplicationService
    {
        public TestUserApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
