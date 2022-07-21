using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.Business.Contract;

/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">增改查的输出、输出模型类型。</typeparam>
public interface ICrudBusinessService<TKey, TModel>
    : ICrudBusinessService<TKey, TModel, TModel, TModel>, IReadOnlyBusinessService<TKey, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{

}
/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOrListOutput">列表或详情的输出模型类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public interface ICrudBusinessService<TKey, TDetailOrListOutput, TListSearchInput, TCreateOrUpdateInput>
    : ICrudBusinessService<TKey, TDetailOrListOutput, TDetailOrListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, IReadOnlyBusinessService<TKey, TDetailOrListOutput, TListSearchInput>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TDetailOrListOutput : notnull
    where TCreateOrUpdateInput : notnull
{

}

/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public interface ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput>
    : ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : notnull
    where TDetailOutput : notnull
    where TCreateOrUpdateInput : notnull
{

}

/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">单个详情的输出类型。</typeparam>
/// <typeparam name="TListOutput">列表的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public interface ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput> : IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : notnull
    where TDetailOutput : notnull
    where TCreateInput : notnull
    where TUpdateInput : notnull
{
    /// <summary>
    /// 以异步的方式创建指定的输入模型对象。
    /// </summary>
    /// <param name="model">要创建的模型。</param>
    /// <returns>一个创建方法，返回 <see cref="Return"/> 结果。</returns>
    [Post]
    ValueTask<Return<TDetailOutput>> CreateAsync([Body] TCreateInput model);
    /// <summary>
    /// 以异步的方式更新指定 id 的输入模型对象。
    /// </summary>
    /// <param name="id">要更新的 id。</param>
    /// <param name="model">要更新的输入模型。</param>
    /// <returns>一个更新方法，返回 <see cref="Return"/> 结果。</returns>
    [Put("{id}")]
    ValueTask<Return<TDetailOutput>> UpdateAsync(TKey id, [Body] TUpdateInput model);
    /// <summary>
    /// 以异步的方式删除指定的 id。
    /// </summary>
    /// <param name="id">要删除的 id。</param>
    /// <returns>一个删除方法，返回 <see cref="Return"/> 结果。</returns>
    [Delete("{id}")]
    ValueTask<Return<TDetailOutput>> DeleteAsync(TKey id);
}
