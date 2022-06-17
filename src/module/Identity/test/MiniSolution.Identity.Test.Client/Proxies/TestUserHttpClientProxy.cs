using MiniSolution.Identity.ApplicationContracts.UserManagement;
using MiniSolution.Identity.HttpApi.Client;

namespace MiniSolution.Identity.Test.ClientWeb.Proxies
{
    public class TestUserHttpClientProxy : UserHttpClientProxy<Guid>, IUserApplicationService<Guid>
    {
        public TestUserHttpClientProxy(IServiceProvider services) : base(services)
        {
        }
    }
}