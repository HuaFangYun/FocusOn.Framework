using Boloni.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Boloni.Client.Http.Abstractions;

public abstract class ClientProxyBase:ApplicationServiceBase, IDisposable
{
    protected ClientProxyBase(IServiceProvider services) : base(services)
    {
    }

    protected IHttpClientFactory ClientFactory => Services.GetRequiredService<IHttpClientFactory>();

    protected virtual string? Name { get; }

    protected HttpClient Client => ClientFactory.CreateClient(Name??Options.DefaultName);

    protected virtual Uri BaseAddress => Client.BaseAddress??throw new ArgumentNullException(nameof(Client.BaseAddress));

    public void Dispose()
    {
        Client?.Dispose();
    }
}