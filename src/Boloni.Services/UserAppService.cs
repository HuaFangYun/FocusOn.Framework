using Boloni.Data;

namespace Boloni.Services;
public class UserAppService : ApplicationServiceBase<BoloniDbContext, User>
{
    public UserAppService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
