using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.Business.Contract;
/// <summary>
/// 提供更新的业务功能。
/// </summary>
/// <typeparam name="TDetailOutput">详情输出类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TUpdateInput">更新输入类型。</typeparam>
public interface IUpdateBusinessService<TDetailOutput, in TKey, in TUpdateInput>
    where TDetailOutput : notnull
    where TKey : IEquatable<TKey>
    where TUpdateInput : notnull
{
    /// <summary>
    /// 以异步的方式更新指定 id 的输入模型对象。
    /// </summary>
    /// <param name="id">要更新的 id。</param>
    /// <param name="model">要更新的输入模型。</param>
    /// <returns>一个更新方法，返回 <see cref="Return"/> 结果。</returns>
    [Put("{id}")]
    ValueTask<Return<TDetailOutput>> UpdateAsync(TKey id, [Body] TUpdateInput model);
}
