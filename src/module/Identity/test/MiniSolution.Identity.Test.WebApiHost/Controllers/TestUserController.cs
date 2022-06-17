using MiniSolution.Identity.HttpApi;
using MiniSolution.Identity.Test.WebApiHost.ApplicationServices;

namespace MiniSolution.Identity.Test.WebApiHost.Controllers
{
    public class TestUserController:UserApiControllerBase<ITestUserApplicationService,Guid>
    {
    }
}
