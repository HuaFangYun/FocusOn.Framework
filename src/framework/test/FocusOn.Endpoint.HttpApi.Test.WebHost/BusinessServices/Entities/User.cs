using FocusOn.Framework.Business.Store;
using FocusOn.Framework.Business.Store.Identity;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities
{
    public class User:IdentityUser<Guid>
    {
        public string Name { get; set; }
    }
}
