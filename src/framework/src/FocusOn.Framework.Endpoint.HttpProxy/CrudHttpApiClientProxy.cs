using System.Net.Http.Json;

using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.DTO;

namespace FocusOn.Framework.Endpoint.HttpProxy;

/// <summary>
/// 提供 CRUD 对 HTTP API 客户端代理的功能，这是一个抽象类。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">输入、输出模型。</typeparam>
public abstract class CrudHttpApiClientProxy<TKey, TModel> : CrudHttpApiClientProxy<TKey, TModel, TModel, TModel, TModel>,ICrudBusinessService<TKey,TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{
    /// <summary>
    /// 初始化 <see cref="CrudHttpApiClientProxy{TKey, TModel}"/> 类的新实例。
    /// </summary>
    /// <param name="services"><inheritdoc/></param>
    protected CrudHttpApiClientProxy(IServiceProvider services) : base(services)
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
/// <typeparam name="TCreateOrUpdateInput">创建数据的输入类型。</typeparam>
public abstract class CrudHttpApiClientProxy<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput> : CrudHttpApiClientProxy<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    where TKey : IEquatable<TKey>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateOrUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="CrudHttpApiClientProxy{TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    /// <param name="services"><inheritdoc/></param>
    protected CrudHttpApiClientProxy(IServiceProvider services) : base(services)
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
    public abstract class CrudHttpApiClientProxy<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput> : ReadOnlyHttpApiClientProxy<TKey,TGetOutput,TGetListOutput,TGetListInput>, ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TKey:IEquatable<TKey>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="CrudHttpApiClientProxy{TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    /// <param name="services"><inheritdoc/></param>
    protected CrudHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }

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

}
