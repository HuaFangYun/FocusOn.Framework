using MiniSolution.Identity.ApplicationContracts.UserManagement;

namespace MiniSolution.Identity.Test.WebApiHost.ApplicationServices
{
    public interface ITestUserApplicationService:IUserApplicationService<Guid>
    {
    }
}
