﻿using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

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

    public virtual async Task<Return> SendAsync(HttpRequestMessage request)
    {
        using var client = CreateClient();
        var response = await client.SendAsync(request);
        return await HandleResultAsync(response);
    }

    public virtual async Task<Return<TResult>> SendAsync<TResult>(HttpRequestMessage request)
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
    protected async Task<Return> HandleResultAsync(HttpResponseMessage response)
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
                return Return.Failed(Logger, "Content from HttpContent is null or empty");
            }
            var result = JsonConvert.DeserializeObject<Return>(content);
            if (result is null)
            {
                return Return.Failed(Logger, $"Deserialize {nameof(Return)} from content failed");
            }
            if (!result.Succeed)
            {
                Logger.LogDebug(JsonConvert.SerializeObject(result));
            }
            return result;
        }
        catch (Exception ex)
        {
            return Return.Failed(Logger, ex);
        }
    }


    /// <summary>
    /// 以异步的方式处理 <see cref="HttpResponseMessage"/> 并解析为 <see cref="Return"/> 对象。
    /// </summary>
    /// <param name="response">HTTP 请求的响应消息。</param>
    /// <exception cref="ArgumentNullException"><paramref name="response"/> 是 null。</exception>
    protected async Task<Return<TResult>> HandleResultAsync<TResult>(HttpResponseMessage response)
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
                return Return<TResult>.Failed(Logger, "Content from HttpContent is null or empty");
            }
            var result = JsonConvert.DeserializeObject<Return<TResult>>(content);
            if (result is null)
            {
                return Return<TResult>.Failed(Logger, $"Deserialize {nameof(Return)} from content failed");
            }
            if (!result.Succeed)
            {
                Logger.LogDebug(JsonConvert.SerializeObject(result));
            }
            return result;
        }
        catch (Exception ex)
        {
            return Return<TResult>.Failed(Logger, ex);
        }
    }
}
