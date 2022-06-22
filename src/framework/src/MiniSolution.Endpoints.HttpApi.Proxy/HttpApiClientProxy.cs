using MiniSolution.Business.Contracts;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MiniSolution.Endpoints.HttpApi.Proxy;

/// <summary>
/// 表示 HTTP API 的客户端代理。这是一个抽象类。
/// </summary>
public abstract class HttpApiClientProxy : BusinessService, IHttpApiClientProxy
{
    /// <summary>
    /// 初始化 <see cref="HttpApiClientProxy"/> 类的新实例。
    /// </summary>
    /// <param name="services"><see cref="IServiceProvider"/> 实例。</param>
    protected HttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }

    /// <summary>
    /// 获取注入的 <see cref="IHttpClientFactory"/> 的服务。
    /// </summary>
    public virtual IHttpClientFactory HttpClientFactory => ServiceProvider.GetRequiredService<IHttpClientFactory>();

    /// <summary>
    /// 获取 <see cref="HttpClient"/> 注入服务时的名称。
    /// </summary>
    protected virtual string Name { get; } = Options.DefaultName;
    /// <summary>
    /// 获取从 <see cref="HttpClientFactory"/> 创建的指定 <see cref="Name"/> 的 <see cref="HttpClient"/> 实例。
    /// </summary>
    protected HttpClient Client => HttpClientFactory.CreateClient(Name);

    /// <summary>
    /// 获取基本请求的 URI 地址。
    /// </summary>
    protected virtual Uri BaseAddress => Client.BaseAddress ?? throw new ArgumentNullException(nameof(Client.BaseAddress));


}