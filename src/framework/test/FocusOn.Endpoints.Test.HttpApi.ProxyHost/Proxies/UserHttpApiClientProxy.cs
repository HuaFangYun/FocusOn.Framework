using FocusOn.Framework.Business.Contract.DTO;
using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;
using FocusOn.Framework.Endpoint.HttpProxy;

namespace FocusOn.Framework.Endpoint.HttpProxy.Test.Host.Proxies;

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
