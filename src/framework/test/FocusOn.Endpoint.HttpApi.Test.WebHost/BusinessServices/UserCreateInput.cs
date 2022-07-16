using FocusOn.Framework.Business.Contract.Identity.DTO;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;
public class UserCreateInput : IdentityUserCreateInput
{
    public string Name { get; set; }
}
