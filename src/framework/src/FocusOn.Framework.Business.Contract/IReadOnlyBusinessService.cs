
using FocusOn.Framework.Business.Contract.DTO;

namespace FocusOn.Framework.Business.Contract;

/// <summary>
/// 提供具备查询查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">输出、输出模型。</typeparam>
public interface IReadOnlyBusinessService<TKey,TModel>:IReadOnlyBusinessService<TKey,TModel,TModel,TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{

}

/// <summary>
/// 提供具备查询查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
public interface IReadOnlyBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput>:IBusinessSerivce
    
    where TKey : IEquatable<TKey>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
{
    /// <summary>
    /// 以异步的方式获取指定 id 的结果。
    /// </summary>
    /// <param name="id">要获取的 id 。</param>
    /// <returns>一个获取结果的方法，返回 <see cref="OutputResult{TGetOutput}"/> 结果。</returns>
    ValueTask<OutputResult<TGetOutput>> GetAsync(TKey id);
    /// <summary>
    /// 以异步的方式获取指定分页的结果列表。
    /// </summary>
    /// <param name="model">获取列表的过滤输入模型。</param>
    /// <returns>一个获取分页结果的方法，返回 <see cref="OutputResult{PagedOutputDto{TGetListOutput}}"/> 结果。</returns>
    Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync(TGetListInput? model = default);
}
