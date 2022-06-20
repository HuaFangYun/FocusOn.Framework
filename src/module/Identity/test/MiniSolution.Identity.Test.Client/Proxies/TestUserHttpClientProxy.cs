using MiniSolution.Identity.Business.Contracts.UserManagement;
using MiniSolution.Identity.Endpoints.HttpApi.Client;

namespace MiniSolution.Identity.Test.ClientWeb.Proxies
{
    public class TestUserHttpClientProxy : UserHttpClientProxy<Guid>, IUserApplicationService<Guid>
    {
        public TestUserHttpClientProxy(IServiceProvider services) : base(services)
        {
        }
    }
}