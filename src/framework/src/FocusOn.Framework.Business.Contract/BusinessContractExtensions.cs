using FocusOn.Framework.Business.Contract.DTO;

namespace FocusOn.Framework.Business.Contract;

/// <summary>
/// 契约的扩展。
/// </summary>
public static class BusinessContractExtensions
{
    /// <summary>
    /// 以异步的方式获取指定条件的结果列表。
    /// </summary>
    /// <typeparam name="TKey">主键类型。</typeparam>
    /// <typeparam name="TDetailOutput">详情的输出类型。</typeparam>
    /// <typeparam name="TListOutput">列表的输出类型。</typeparam>
    /// <typeparam name="TListSearchInput">列表查询的输入类型。</typeparam>
    /// <param name="readOnlyBusinessService">只读业务契约服务的扩展。</param>
    /// <param name="model">获取列表的过滤输入模型。</param>
    /// <returns>一个获取列表结果的方法，返回 <see cref="OutputResult{TListOutput}"/> 结果。</returns>
    /// <returns></returns>
    public static async Task<OutputResult<IReadOnlyList<TListOutput>>> GetListAsync<TKey, TDetailOutput, TListOutput, TListSearchInput>(this IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput> readOnlyBusinessService, TListSearchInput model)
        where TKey : IEquatable<TKey>
        where TDetailOutput : class
        where TListOutput : class
        where TListSearchInput : class
    {
        var result = await readOnlyBusinessService.GetListAsync(model);

        return OutputResult<IReadOnlyList<TListOutput>>.Success(result.Data.Items);
    }
}
