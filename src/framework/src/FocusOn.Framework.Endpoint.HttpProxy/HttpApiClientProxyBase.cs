using System.Net.Http.Json;
using System.Text.Json;

using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.DTO;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace FocusOn.Framework.Endpoint.HttpProxy;

/// <summary>
/// 表示 HTTP API 的客户端代理。这是一个抽象类。
/// </summary>
public abstract class HttpApiClientProxyBase : BusinessServiceBase, IHttpApiClientProxy
{
    /// <summary>
    /// 初始化 <see cref="HttpApiClientProxyBase"/> 类的新实例。
    /// </summary>
    /// <param name="services"><see cref="IServiceProvider"/> 实例。</param>
    protected HttpApiClientProxyBase(IServiceProvider services) : base(services)
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
    protected virtual Uri BaseAddress =>Client.BaseAddress ?? throw new ArgumentNullException(nameof(Client.BaseAddress));


    /// <summary>
    /// 获取 HTTP API 的根路径。
    /// </summary>
    protected virtual string? RootPath { get; }


    /// <summary>
    /// 获取请求的 URI。
    /// </summary>
    /// <param name="queryParameters">一个查询参数对象，属性字段将生成为 query 部分的 key=value 字符串。</param>
    /// <param name="relativeUri">请求的 HTTP API 路由。该路由是相对路径，并且会与 <see cref="RootPath"/> 的值组合成最终请求路由。</param>
    /// <returns>请求 HTTP API 的 URI 资源。</returns>
    protected Uri GetRequestUri(string? relativeUri = default, object? queryParameters = default)
    {
        string? queryString = default;
        if (queryParameters is not null)
        {
            queryString = queryParameters.GetType().GetProperties().Where(p => p.CanRead).Select(m => $"{m.Name}={m.GetValue(queryParameters)}").Aggregate((prev, next) => $"{prev}&{next}");
        }

        var hasQueryMark = relativeUri?.IndexOf('?') > -1;


        return new(BaseAddress, $"{RootPath}/{relativeUri}{(hasQueryMark ? queryString : $"?{queryString}")}");
    }

    /// <summary>
    /// 以异步的方式处理 <see cref="HttpResponseMessage"/> 并解析为 <see cref="OutputResult"/> 对象。
    /// </summary>
    /// <param name="response">HTTP 请求的响应消息。</param>
    /// <exception cref="ArgumentNullException"><paramref name="response"/> 是 null。</exception>
    protected async Task<OutputResult> HandleOutputResultAsync(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        LogRequestUri(response);

        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content.IsNullOrEmpty())
            {
                return OutputResult.Failed(Logger, "Content from HttpContent is null or empty");
            }
            var result = JsonConvert.DeserializeObject<OutputResult>(content);
            if (result is null)
            {
                return OutputResult.Failed(Logger, $"Deserialize {nameof(OutputResult)} from content failed");
            }
            if (!result.Succeed)
            {
                Logger.LogDebug(JsonConvert.SerializeObject(result));
            }
            return result;
        }
        catch (Exception ex)
        {
            return OutputResult.Failed(Logger, ex);
        }
    }

    /// <summary>
    /// 以异步的方式处理 <see cref="HttpResponseMessage"/> 并解析为 <see cref="OutputResult{TResult}"/> 对象。
    /// </summary>
    /// <param name="response">HTTP 请求的响应消息。</param>
    /// <exception cref="ArgumentNullException"><paramref name="response"/> 是 null。</exception>
    protected async Task<OutputResult<TResult>> HandleOutputResultAsync<TResult>(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        LogRequestUri(response);

        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content.IsNullOrEmpty())
            {
                return OutputResult<TResult>.Failed(Logger, "Content from HttpContent is null or empty");
            }
            var result = JsonConvert.DeserializeObject<OutputResult<TResult>>(content);
            if (result is null)
            {
                return OutputResult<TResult>.Failed(Logger, $"Deserialize {nameof(OutputResult)} from content failed");
            }
            if (!result.Succeed)
            {
                Logger.LogDebug(JsonConvert.SerializeObject(result));
            }
            return result;

        }
        catch (Exception ex)
        {
            return OutputResult<TResult>.Failed(Logger, ex);
        }
    }

    /// <summary>
    /// 记录 HTTP 请求的绝对路径。
    /// </summary>
    /// <param name="response">响应结果。</param>
    void LogRequestUri(HttpResponseMessage response) => Logger.LogError("Request uri is {0}", response?.RequestMessage?.RequestUri?.AbsolutePath);

    /// <summary>
    /// 发送指定请求消息并返回 <see cref="OutputResult{TResult}"/> 结果。
    /// </summary>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="request">请求消息。</param>
    /// <param name="option">请求完成选项。</param>
    protected virtual async Task<OutputResult<TResult>> SendAsync<TResult>(HttpRequestMessage request, HttpCompletionOption option = HttpCompletionOption.ResponseContentRead)
    {
        try
        {
            var response = await Client.SendAsync(request, option, CancellationToken);
            return await HandleOutputResultAsync<TResult>(response);
        }
        catch (Exception ex)
        {
            return OutputResult<TResult>.Failed(Logger, ex);
        }
    }

    /// <summary>
    /// 发送指定请求消息并返回 <see cref="OutputResult"/> 结果。
    /// </summary>
    /// <param name="request">请求消息。</param>
    /// <param name="option">请求完成选项。</param>
    protected virtual async Task<OutputResult> SendAsync(HttpRequestMessage request, HttpCompletionOption option = HttpCompletionOption.ResponseContentRead)
    {
        try
        {
            var response = await Client.SendAsync(request, option, CancellationToken);
            return await HandleOutputResultAsync(response);
        }
        catch (Exception ex)
        {
            return OutputResult.Failed(Logger, ex);
        }
    }

    /// <summary>
    /// 以 JSON 方法发送 POST 请求。
    /// </summary>
    /// <typeparam name="TValue">值的类型。</typeparam>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="requestUri">请求地址。</param>
    /// <param name="value">发送的值。</param>
    /// <param name="options">JSON 序列号配置。</param>
    /// <returns></returns>
    protected virtual async Task<OutputResult<TResult>> PostAsync<TValue, TResult>(string requestUri, TValue value, JsonSerializerOptions? options = default)
    {
        try
        {
            var response = await Client.PostAsJsonAsync(requestUri, value, options, CancellationToken);
            return await HandleOutputResultAsync<TResult>(response);
        }
        catch (Exception ex)
        {
            return OutputResult<TResult>.Failed(Logger, ex);
        }
    }

    /// <summary>
    /// 以 JSON 方法发送 POST 请求。
    /// </summary>
    /// <typeparam name="TValue">值的类型。</typeparam>
    /// <param name="requestUri">请求地址。</param>
    /// <param name="value">发送的值。</param>
    /// <param name="options">JSON 序列号配置。</param>
    /// <returns></returns>
    protected virtual async Task<OutputResult> PostAsync<TValue>(string requestUri, TValue value, JsonSerializerOptions? options = default)
    {
        try
        {
            var response = await Client.PostAsJsonAsync(requestUri, value, options, CancellationToken);
            return await HandleOutputResultAsync(response);
        }
        catch (Exception ex)
        {
            return OutputResult.Failed(Logger, ex);
        }
    }

    /// <summary>
    /// 发送 GET 请求。
    /// </summary>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="requestUri">请求地址。</param>
    /// <returns></returns>
    protected virtual async Task<OutputResult<TResult>> GetAsync<TResult>(string requestUri)
    {
        try
        {
            var response = await Client.GetAsync(requestUri, CancellationToken);
            return await HandleOutputResultAsync<TResult>(response);
        }
        catch (Exception ex)
        {
            return OutputResult<TResult>.Failed(Logger, ex);
        }
    }
    /// <summary>
    /// 发送 GET 请求。
    /// </summary>
    /// <param name="requestUri">请求地址。</param>
    /// <returns></returns>
    protected virtual async Task<OutputResult> GetAsync(string requestUri)
    {
        try
        {
            var response = await Client.GetAsync(requestUri, CancellationToken);
            return await HandleOutputResultAsync(response);
        }
        catch (Exception ex)
        {
            return OutputResult.Failed(Logger, ex);
        }
    }
    /// <summary>
    /// 以 JSON 的方式发送 PUT 请求。
    /// </summary>
    /// <typeparam name="TValue">值的类型。</typeparam>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="requestUri">请求地址。</param>
    /// <param name="value">发送的值。</param>
    /// <param name="options">JSON 序列号配置。</param>
    protected virtual async Task<OutputResult<TResult>> PutAsync<TValue, TResult>(string requestUri, TValue value, JsonSerializerOptions? options = default)
    {
        try
        {
            var response = await Client.PutAsJsonAsync(requestUri, value, options, CancellationToken);
            return await HandleOutputResultAsync<TResult>(response);
        }
        catch (Exception ex)
        {
            return OutputResult<TResult>.Failed(Logger, ex);
        }
    }
    /// <summary>
    /// 以 JSON 的方式发送 PUT 请求。
    /// </summary>
    /// <typeparam name="TValue">值的类型。</typeparam>
    /// <param name="requestUri">请求地址。</param>
    /// <param name="value">发送的值。</param>
    /// <param name="options">JSON 序列号配置。</param>
    protected virtual async Task<OutputResult> PutAsync<TValue>(string requestUri, TValue value, JsonSerializerOptions? options = default)
    {
        try
        {
            var response = await Client.PutAsJsonAsync(requestUri, value, options, CancellationToken);
            return await HandleOutputResultAsync(response);
        }
        catch (Exception ex)
        {
            return OutputResult.Failed(Logger, ex);
        }
    }
    /// <summary>
    /// 发送 DELETE 请求。
    /// </summary>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="requestUri">请求地址。</param>
    protected virtual async Task<OutputResult<TResult>> DeleteAsync<TResult>(string requestUri)
    {
        try
        {
            var response = await Client.DeleteAsync(requestUri, CancellationToken);
            return await HandleOutputResultAsync<TResult>(response);
        }
        catch (Exception ex)
        {
            return OutputResult<TResult>.Failed(Logger, ex);
        }
    }
    /// <summary>
    /// 发送 DELETE 请求。
    /// </summary>
    /// <param name="requestUri">请求地址。</param>
    protected virtual async Task<OutputResult> DeleteAsync(string requestUri)
    {
        try
        {
            var response = await Client.DeleteAsync(requestUri, CancellationToken);
            return await HandleOutputResultAsync(response);
        }
        catch (Exception ex)
        {
            return OutputResult.Failed(Logger, ex);
        }
    }
}