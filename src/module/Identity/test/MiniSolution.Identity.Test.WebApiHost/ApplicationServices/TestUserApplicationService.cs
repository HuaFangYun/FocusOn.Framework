using FocusOn.Business.Contracts;
using FocusOn.Identity.Business.Services.UserManagement;
using FocusOn.Identity.Test.WebApiHost.Entities;

namespace FocusOn.Identity.Test.WebApiHost.Business.Services
{
    public class TestUserApplicationService : EfCoreUserBusinessService<TestIdentityDbContext, TestUser, Guid>, ITestUserApplicationService,IRemotingService
    {
        public TestUserApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
