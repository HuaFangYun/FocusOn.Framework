using MiniSolution.Business.Contracts;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MiniSolution.Endpoints.HttpApi.Proxy;

public abstract class HttpApiClientProxy : BusinessService, IHttpApiClientProxy
{
    protected HttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }

    public IHttpClientFactory HttpClientFactory => ServiceProvider.GetRequiredService<IHttpClientFactory>();

    protected virtual string? Name { get; }

    protected HttpClient Client => HttpClientFactory.CreateClient(Name ?? Options.DefaultName);

    protected virtual Uri BaseAddress => Client.BaseAddress ?? throw new ArgumentNullException(nameof(Client.BaseAddress));


}