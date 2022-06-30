using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FocusOn.Framework.Business.Contract.DTO;

namespace FocusOn.Framework.Endpoint.HttpProxy;

public abstract class ReadOnlyHttpApiClientProxy<TKey, TModel> : ReadOnlyHttpApiClientProxy<TKey, TModel, TModel, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{
    protected ReadOnlyHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }
}

/// <summary>
/// 表示只读查询的 HTTP API 客户端代理。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
public abstract class ReadOnlyHttpApiClientProxy<TKey, TGetOutput, TGetListOutput, TGetListInput> : HttpApiClientProxy
    where TKey : IEquatable<TKey>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
{
    protected ReadOnlyHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }

    /// <summary>
    /// 以异步的方式使用 HttpGet 方式请求 HTTP API 获取指定 id 的数据。
    /// </summary>
    public virtual async ValueTask<OutputResult<TGetOutput?>> GetAsync(TKey id)
    {
        var response = await Client.GetAsync(GetRequestUri(id.ToString()));
        return await HandleOutputResultAsync<TGetOutput?>(response);
    }

    /// <summary>
    /// 以异步的方式使用 HttpGet 方式请求 HTTP API 获取指定数据筛选输入的数据。
    /// </summary>
    public virtual async Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync(TGetListInput model)
    {
        var uri = GetRequestUri(queryParameters: model);
        var response = await Client.GetAsync(uri);
        return await HandleOutputResultAsync<PagedOutputDto<TGetListOutput>>(response);
    }
}
