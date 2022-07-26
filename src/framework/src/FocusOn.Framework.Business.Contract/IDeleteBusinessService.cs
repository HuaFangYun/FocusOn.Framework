using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.Business.Contract;

/// <summary>
/// 提供删除的业务功能。
/// </summary>
/// <typeparam name="TDetailOutput">详情类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public interface IDeleteBusinessService<TDetailOutput, in TKey>
    where TDetailOutput : notnull
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 以异步的方式删除指定的 id。
    /// </summary>
    /// <param name="id">要删除的 id。</param>
    /// <returns>一个删除方法，返回 <see cref="Return"/> 结果。</returns>
    [Delete("{id}")]
    Task<Return<TDetailOutput>> DeleteAsync(TKey id);
}
