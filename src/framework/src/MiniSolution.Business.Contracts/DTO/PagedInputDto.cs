namespace MiniSolution.Business.Contracts.DTO;

/// <summary>
/// 表示分页输入。
/// </summary>
public class PagedInputDto
{
    /// <summary>
    /// 获取或设置当前页码。默认是 1。
    /// </summary>
    public int Page { get; set; } = 1;
    /// <summary>
    /// 获取或设置每一页的数据量。默认是 10。
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// 设置最大的数据量。
    /// </summary>
    public void SetMaxSize() => Size = int.MaxValue;
}
