using FocusOn.Framework.Business.Store.Identity.Configurations;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices.Entities;
public class UserConfiguration : IdentityUserConfiguration<User, Guid>
{
}
