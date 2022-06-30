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
    protected virtual Uri BaseAddress =>new Uri(Client.BaseAddress,RootPath) ?? throw new ArgumentNullException(nameof(Client.BaseAddress));


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
    protected Uri GetRequestUri(string relativeUri = default, object queryParameters = default)
    {
        string? queryString = default;
        if (queryParameters is not null)
        {
            queryString = queryParameters.GetType().GetProperties().Where(p => p.CanRead).Select(m => $"{m.Name}={m.GetValue(queryParameters)}").Aggregate((prev, next) => $"{prev}&{next}");
        }

        var hasQueryMark = relativeUri is not null && relativeUri.IndexOf('?') > -1;


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
    protected void LogRequestUri(HttpResponseMessage response) => Logger.LogError("Request uri is {0}", response?.RequestMessage?.RequestUri?.AbsolutePath);

}