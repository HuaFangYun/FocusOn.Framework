using FocusOn.Business.Contracts.DTO;
using FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices;
using FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;
using FocusOn.Endpoints.HttpApi.Proxy;

namespace FocusOn.Endpoints.Test.HttpApi.ProxyHost.Proxies
{
    public class UserHttpApiClientProxy : CrudHttpApiClientProxy<Guid, User>, ITestUserBusinessService
    {
        public UserHttpApiClientProxy(IServiceProvider services) : base(services)
        {
        }

        protected override string? RootPath => "user";

        public async Task<OutputResult<User>> GetByNameAsync(string name)
        {
            var url = GetRequestUri($"by-name/{name}");
            var result = await Client.GetAsync(url);
            return await HandleOutputResultAsync<User>(result);
        }
    }
}
