using FocusOn.Framework.Business.Contract.DTO;
using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.Business.Contract;

/// <summary>
/// 提供具备查询查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">详情、列表的输出类型模型和列表查询的输入模型类型。</typeparam>
public interface IReadOnlyBusinessService<TKey, TModel> : IReadOnlyBusinessService<TKey, TModel, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{

}

/// <summary>
/// 提供具备查询查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOrListOutput">列表或详情的输出模型类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询的输入类型。</typeparam>
public interface IReadOnlyBusinessService<TKey, TDetailOrListOutput, TListSearchInput> : IReadOnlyBusinessService<TKey, TDetailOrListOutput, TDetailOrListOutput, TListSearchInput>
    where TKey : IEquatable<TKey>
    where TDetailOrListOutput : class
    where TListSearchInput : class
{

}
/// <summary>
/// 提供具备查询查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">详情的输出类型。</typeparam>
/// <typeparam name="TListOutput">列表的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询的输入类型。</typeparam>
public interface IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput> : IBusinessSerivce

    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : class
    where TDetailOutput : class
{
    /// <summary>
    /// 以异步的方式获取指定 id 的结果。
    /// </summary>
    /// <param name="id">要获取的 id 。</param>
    /// <returns>一个获取结果的方法，返回 <see cref="OutputResult{TDetailOutput}"/> 结果。</returns>
    [Get("{id}")]
    ValueTask<OutputResult<TDetailOutput?>> GetAsync(TKey id);
    /// <summary>
    /// 以异步的方式获取指定分页的结果列表。
    /// </summary>
    /// <param name="model">获取列表的过滤输入模型。</param>
    /// <returns>一个获取分页结果的方法，返回 <see cref="OutputResult{TListOutput}"/> 结果。</returns>
    [Get]
    Task<OutputResult<PagedOutput<TListOutput>>> GetListAsync([Query] TListSearchInput? model = default);
}
