using FocusOn.Framework.Business.Store;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities
{
    public class User:EntityBase<Guid>
    {
        public string Name { get; set; }
    }
}
