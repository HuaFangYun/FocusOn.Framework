using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace FocusOn.Framework.Endpoint.HttpProxy.Dynamic;

/// <summary>
/// 表示对指定的服务进行 HTTP 动态请求的代理服务。
/// </summary>
/// <typeparam name="TService"></typeparam>
internal class DynamicHttpClientProxy<TService> : IHttpApiClientProxy
{
    public DynamicHttpClientProxy(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    protected IServiceProvider ServiceProvider { get; }
    public IHttpClientFactory HttpClientFactory => ServiceProvider.GetRequiredService<IHttpClientFactory>();

    protected ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();

    public ILogger Logger => LoggerFactory.CreateLogger(GetType().Name);

    public DynamicHttpProxyOptions Options => ServiceProvider.GetRequiredService<IOptions<DynamicHttpProxyOptions>>().Value;

    protected virtual HttpClient CreateClient()
    {
        var serviceType = typeof(TService);
        var configuration = Options.HttpProxies[serviceType];
        var client = HttpClientFactory.CreateClient(configuration.Name);
        client.BaseAddress = new(configuration.BaseAddress);
        return client;
    }

    public virtual async Task SendAsync(HttpRequestMessage request)
    {
        using var client = CreateClient();
        var response = await client.SendAsync(request);
        await HandleResultAsync(response);
    }

    public virtual async Task<TResult> SendAsync<TResult>(HttpRequestMessage request) where TResult : notnull
    {
        using var client = CreateClient();
        var response = await client.SendAsync(request);
        return await HandleResultAsync<TResult>(response);
    }

    /// <summary>
    /// 以异步的方式处理 <see cref="HttpResponseMessage"/> 并解析为 <see cref="Return"/> 对象。
    /// </summary>
    /// <param name="response">HTTP 请求的响应消息。</param>
    /// <exception cref="ArgumentNullException"><paramref name="response"/> 是 null。</exception>
    protected async Task HandleResultAsync(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content.IsNullOrEmpty())
            {
                Logger.LogError("Content from HttpContent is null or empty");
                return;
            }
            var result = JsonConvert.DeserializeObject<Return>(content);
            if (result is null)
            {
                Logger.LogError($"Deserialize from content failed with JSON string: {content}");
                return;
            }
            return;
        }
        catch (Exception ex)
        {
            Logger.LogCritical(ex.Message, ex);
            return;
        }
    }


    /// <summary>
    /// 以异步的方式处理 <see cref="HttpResponseMessage"/> 并解析为 <see cref="Return"/> 对象。
    /// </summary>
    /// <param name="response">HTTP 请求的响应消息。</param>
    /// <exception cref="ArgumentNullException"><paramref name="response"/> 是 null。</exception>
    protected async Task<TResult> HandleResultAsync<TResult>(HttpResponseMessage response) where TResult : notnull
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content.IsNullOrEmpty())
            {
                Logger.LogError("Content from HttpContent is null or empty");
                return default;
            }
            var result = JsonConvert.DeserializeObject<TResult>(content);
            if (result is null)
            {
                Logger.LogError($"Deserialize from content failed with JSON string: {content}");
                return default;
            }
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogCritical(ex.Message, ex);
            return default;
        }
    }
}
