namespace FocusOn.Framework.Business.Contract.DTO;
/// <summary>
/// 输出结果的传输对象基类。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
public abstract class OutputBase<TKey>
{
    /// <summary>
    /// 获取或设置主键类型。
    /// </summary>
    public TKey Id { get; set; }
}
