using FocusOn.Framework.Business.Contract;

namespace FocusOn.Framework.Endpoint.HttpProxy;

/// <summary>
/// 提供 CRUD 对 HTTP API 客户端代理的功能，这是一个抽象类。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">输入、输出模型。</typeparam>
public abstract class CrudHttpApiClientProxyBase<TKey, TModel> : CrudHttpApiClientProxyBase<TKey, TModel, TModel, TModel, TModel>, ICrudBusinessService<TKey, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{
    /// <summary>
    /// 初始化 <see cref="CrudHttpApiClientProxyBase{TKey, TModel}"/> 类的新实例。
    /// </summary>
    /// <param name="services"><inheritdoc/></param>
    protected CrudHttpApiClientProxyBase(IServiceProvider services) : base(services)
    {
    }
}

/// <summary>
/// 提供 CRUD 对 HTTP API 客户端代理的功能，这是一个抽象类。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建数据的输入类型。</typeparam>
public abstract class CrudHttpApiClientProxyBase<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput> : CrudHttpApiClientProxyBase<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : class
    where TDetailOutput : class
    where TCreateOrUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="CrudHttpApiClientProxyBase{TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    /// <param name="services"><inheritdoc/></param>
    protected CrudHttpApiClientProxyBase(IServiceProvider services) : base(services)
    {
    }
}
/// <summary>
/// 提供 CRUD 对 HTTP API 客户端代理的功能，这是一个抽象类。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public abstract class CrudHttpApiClientProxyBase<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput> : ReadOnlyHttpApiClientProxy<TKey, TDetailOutput, TListOutput, TListSearchInput>, ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
where TKey : IEquatable<TKey>
where TListSearchInput : class
where TListOutput : notnull
where TDetailOutput : notnull
where TCreateInput : notnull
where TUpdateInput : notnull
{
    /// <summary>
    /// 初始化 <see cref="CrudHttpApiClientProxyBase{TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    /// <param name="services"><inheritdoc/></param>
    protected CrudHttpApiClientProxyBase(IServiceProvider services) : base(services)
    {
    }

    /// <summary>
    /// 以异步的方式使用 HttpPost 方式请求 HTTP API 创建数据。
    /// </summary>
    /// <param name="model">要推送的创建数据模型。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public virtual Task<Return<TDetailOutput>> CreateAsync(TCreateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return Task.FromResult(Return<TDetailOutput>.Failed(errors));
        }

        return PostAsync<TCreateInput, TDetailOutput>(GetRequestUri(), model);
    }

    /// <summary>
    /// 以异步的方式使用 HttpDelete 方式请求 HTTP API 删除指定 id 的数据。
    /// </summary>
    /// <param name="id">要删除的 id。</param>
    public virtual Task<Return<TDetailOutput>> DeleteAsync(TKey id)
        => DeleteAsync<TDetailOutput>(GetRequestUri(id.ToString()));

    /// <summary>
    /// 以异步的方式使用 HttpPut 方式请求 HTTP API 更新指定 id 的数据。
    /// </summary>
    /// <param name="id">要更新的 id。</param>
    /// <param name="model">要更新的数据。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public virtual Task<Return<TDetailOutput>> UpdateAsync(TKey id, TUpdateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return Task.FromResult(Return<TDetailOutput>.Failed(errors));
        }

        return PutAsync<TUpdateInput, TDetailOutput>(GetRequestUri(), model);
    }

}
