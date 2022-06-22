using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using MiniSolution.Business.Contracts;
using MiniSolution.Business.Contracts.DTO;

using Newtonsoft.Json;

namespace MiniSolution.Endpoints.HttpApi.Proxy;
/// <summary>
/// 提供 CRUD 对 HTTP API 客户端代理的功能，这是一个抽象类。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建数据的输入类型。</typeparam>
public abstract class CrudHttpClientProxy<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput> : CrudHttpClientProxy<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateOrUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="CrudHttpClientProxy{TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    /// <param name="services"><inheritdoc/></param>
    protected CrudHttpClientProxy(IServiceProvider services) : base(services)
    {
    }
}
    /// <summary>
    /// 提供 CRUD 对 HTTP API 客户端代理的功能，这是一个抽象类。
    /// </summary>
    /// <typeparam name="TKey">主键类型。</typeparam>
    /// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
    /// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
    /// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
    /// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
    /// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
    public abstract class CrudHttpClientProxy<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput> : HttpApiClientProxy, ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="CrudHttpClientProxy{TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    /// <param name="services"><inheritdoc/></param>
    protected CrudHttpClientProxy(IServiceProvider services) : base(services)
    {
    }

    /// <summary>
    /// 获取 HTTP API 的根路径。
    /// </summary>
    protected virtual string RootPath { get; }

    /// <summary>
    /// 以异步的方式使用 HttpPost 方式请求 HTTP API 创建数据。
    /// </summary>
    /// <param name="model">要推送的创建数据模型。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public virtual async ValueTask<OutputResult> CreateAsync(TCreateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return OutputResult.Failed(errors);
        }

        var response = await Client.PostAsJsonAsync(GetRequestUri(), model);
        return await HandleOutputResultAsync(response);
    }

    /// <summary>
    /// 以异步的方式使用 HttpDelete 方式请求 HTTP API 删除指定 id 的数据。
    /// </summary>
    /// <param name="id">要删除的 id。</param>
    public virtual async ValueTask<OutputResult> DeleteAsync(TKey id)
    {
        var response = await Client.DeleteAsync(GetRequestUri(id.ToString()));
        return await HandleOutputResultAsync(response);
    }

    /// <summary>
    /// 以异步的方式使用 HttpGet 方式请求 HTTP API 获取指定 id 的数据。
    /// </summary>
    public virtual async ValueTask<OutputResult<TGetOutput?>> GetAsync(TKey id)
    {
        var response = await Client.GetAsync(GetRequestUri(id.ToString()));
        return await GetOutputResultAsync<TGetOutput?>(response);
    }

    /// <summary>
    /// 以异步的方式使用 HttpGet 方式请求 HTTP API 获取指定数据筛选输入的数据。
    /// </summary>
    public virtual async Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync(TGetListInput model)
    {
        var uri = GetRequestUri(queryParameters: model);
        var response = await Client.GetAsync(uri);
        return await GetOutputResultAsync<PagedOutputDto<TGetListOutput>>(response);
    }
    /// <summary>
    /// 以异步的方式使用 HttpPut 方式请求 HTTP API 更新指定 id 的数据。
    /// </summary>
    /// <param name="id">要更新的 id。</param>
    /// <param name="model">要更新的数据。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public virtual async ValueTask<OutputResult> UpdateAsync(TKey id, TUpdateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return OutputResult.Failed(errors);
        }

        var response = await Client.PutAsJsonAsync(GetRequestUri(), model);
        return await HandleOutputResultAsync(response);
    }

    /// <summary>
    /// 获取请求的 URI。
    /// </summary>
    /// <param name="queryParameters">一个查询参数对象，属性字段将生成为 query 部分的 key=value 字符串。</param>
    /// <param name="route">请求的 HTTP API 路由。该路由是相对路径，并且会与 <see cref="RootPath"/> 的值组合成最终请求路由。</param>
    /// <returns>请求 HTTP API 的 URI 资源。</returns>
    protected Uri GetRequestUri(string route = default, object queryParameters = default)
    {
        string? queryString = default;
        if (queryParameters is not null)
        {
            queryString = queryParameters.GetType().GetProperties().Where(p => p.CanRead).Select(m => $"{m.Name}={m.GetValue(queryParameters)}").Aggregate((prev, next) => $"{prev}&{next}");
        }

        var hasQueryMark = route is not null && route.IndexOf('?') > -1;


        return new(BaseAddress, $"{RootPath}/{route}{(hasQueryMark ? queryString : $"?{queryString}")}");
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
    protected async Task<OutputResult<TResult>> GetOutputResultAsync<TResult>(HttpResponseMessage response)
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
