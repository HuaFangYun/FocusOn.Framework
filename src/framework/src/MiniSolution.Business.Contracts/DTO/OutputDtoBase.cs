namespace MiniSolution.Business.Contracts.DTO;
/// <summary>
/// 输出结果的传输对象基类。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
public abstract class OutputDtoBase<TKey>
{
    /// <summary>
    /// 获取或设置主键类型。
    /// </summary>
    public TKey Id { get; set; }
}
