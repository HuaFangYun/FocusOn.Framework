using FocusOn.Framework.Business.Contract.DTO;

namespace FocusOn.Framework.Endpoint.HttpProxy;

/// <summary>
/// 表示只读查询的 HTTP API 客户端代理。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">输入、输出模型。</typeparam>
public abstract class ReadOnlyHttpApiClientProxyBase<TKey, TModel> : ReadOnlyHttpApiClientProxy<TKey, TModel, TModel, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{
    /// <summary>
    /// 初始化 <see cref="ReadOnlyHttpApiClientProxyBase{TKey, TModel}"/> 类的新实例。
    /// </summary>
    protected ReadOnlyHttpApiClientProxyBase(IServiceProvider services) : base(services)
    {
    }
}

/// <summary>
/// 表示只读查询的 HTTP API 客户端代理。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
public abstract class ReadOnlyHttpApiClientProxy<TKey, TDetailOutput, TListOutput, TListSearchInput> : HttpApiClientProxyBase
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : class
    where TDetailOutput : class
{
    /// <summary>
    /// 初始化 <see cref="ReadOnlyHttpApiClientProxy{TKey, TDetailOutput, TListOutput, TListSearchInput}"/> 类的新实例。
    /// </summary>
    protected ReadOnlyHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }

    /// <summary>
    /// 以异步的方式使用 HttpGet 方式请求 HTTP API 获取指定 id 的数据。
    /// </summary>
    public virtual async ValueTask<OutputResult<TDetailOutput?>> GetAsync(TKey id)
    {
        var response = await Client.GetAsync(GetRequestUri(id.ToString()));
        return await HandleOutputResultAsync<TDetailOutput?>(response);
    }

    /// <summary>
    /// 以异步的方式使用 HttpGet 方式请求 HTTP API 获取指定数据筛选输入的数据。
    /// </summary>
    public virtual async Task<OutputResult<PagedOutputDto<TListOutput>>> GetListAsync(TListSearchInput model)
    {
        var uri = GetRequestUri(queryParameters: model);
        var response = await Client.GetAsync(uri);
        return await HandleOutputResultAsync<PagedOutputDto<TListOutput>>(response);
    }
}
