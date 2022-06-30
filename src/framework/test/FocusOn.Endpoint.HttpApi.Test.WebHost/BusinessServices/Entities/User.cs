using FocusOn.Endpoints.HttpApi.Data.Entities;

namespace FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities
{
    public class User:EntityBase<Guid>
    {
        public string Name { get; set; }
    }
}
