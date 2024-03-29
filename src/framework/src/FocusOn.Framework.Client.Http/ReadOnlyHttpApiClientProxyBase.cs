﻿using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.DTO;

namespace FocusOn.Framework.Client.Http;

/// <summary>
/// 表示只读查询的 HTTP API 客户端代理。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">详情、列表的输出类型模型和列表查询的输入模型类型。</typeparam>
public abstract class ReadOnlyHttpApiClientProxyBase<TKey, TModel> : ReadOnlyHttpApiClientProxy<TKey, TModel, TModel>
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
/// <typeparam name="TDetailOrListOutput">列表或详情的输出模型类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询的输入类型。</typeparam>
public abstract class ReadOnlyHttpApiClientProxy<TKey, TDetailOrListOutput, TListSearchInput> : ReadOnlyHttpApiClientProxy<TKey, TDetailOrListOutput, TDetailOrListOutput, TListSearchInput>, IReadOnlyBusinessService<TKey, TDetailOrListOutput, TListSearchInput>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TDetailOrListOutput : notnull
{
    /// <summary>
    /// 初始化 <see cref="ReadOnlyHttpApiClientProxy{TKey, TDetailOrListOutput, TListSearchInput}"/> 类的新实例。
    /// </summary>
    protected ReadOnlyHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }
}
/// <summary>
/// 表示只读查询的 HTTP API 客户端代理。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">详情的输出类型。</typeparam>
/// <typeparam name="TListOutput">列表的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询的输入类型。</typeparam>
public abstract class ReadOnlyHttpApiClientProxy<TKey, TDetailOutput, TListOutput, TListSearchInput> : HttpApiClientProxyBase, IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : notnull
    where TDetailOutput : notnull
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
    public virtual Task<Return<TDetailOutput>> GetAsync(TKey id)
        => GetAsync<TDetailOutput>(GetRequestUri(id.ToString()));

    /// <summary>
    /// 以异步的方式使用 HttpGet 方式请求 HTTP API 获取指定数据筛选输入的数据。
    /// </summary>
    public virtual Task<Return<PagedOutput<TListOutput>>> GetListAsync(TListSearchInput? model = default)
        => GetAsync<PagedOutput<TListOutput>>(GetRequestUri(queryParameters: model));
}
