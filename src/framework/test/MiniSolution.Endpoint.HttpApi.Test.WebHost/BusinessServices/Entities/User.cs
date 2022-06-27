using MiniSolution.Business.Services.Entities;

namespace MiniSolution.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities
{
    public class User:EntityBase<Guid>
    {
        public string Name { get; set; }
    }
}
