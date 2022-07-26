using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.Business.Contract;
/// <summary>
/// 提供创建的业务功能。
/// </summary>
/// <typeparam name="TDetailOutput">详情输出类型。</typeparam>
/// <typeparam name="TCreateInput">创建输入类型。</typeparam>
public interface ICreateBusinessService<TDetailOutput, in TCreateInput>
    where TCreateInput : notnull
    where TDetailOutput : notnull
{
    /// <summary>
    /// 以异步的方式创建指定的输入模型对象。
    /// </summary>
    /// <param name="model">要创建的模型。</param>
    /// <returns>一个创建方法，返回 <see cref="Return"/> 结果。</returns>
    [Post]
    Task<Return<TDetailOutput>> CreateAsync([Body] TCreateInput model);
}
