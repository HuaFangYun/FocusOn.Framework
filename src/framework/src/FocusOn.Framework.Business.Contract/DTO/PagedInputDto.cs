namespace FocusOn.Framework.Business.Contract.DTO;

/// <summary>
/// 表示分页输入。
/// </summary>
public class PagedInputDto
{
    /// <summary>
    /// 初始化 <see cref="PagedInputDto"/> 类的新实例。
    /// </summary>
    public PagedInputDto():this(1,int.MaxValue)
    {
    }

    /// <summary>
    /// 初始化 <see cref="PagedInputDto"/> 类的新实例。
    /// </summary>
    /// <param name="page">当前页码。</param>
    /// <param name="size">每页的数据量。</param>
    public PagedInputDto(int page, int size)
    {
        Page = page;
        Size = size;
    }

    /// <summary>
    /// 获取或设置当前页码。默认是 1。
    /// </summary>
    public int Page { get; set; } = 1;
    /// <summary>
    /// 获取或设置每一页的数据量。默认是 10。
    /// </summary>
    public int Size { get;  set; } = 10;

    /// <summary>
    /// 设置最大的数据量。
    /// </summary>
    public virtual void SetMaxSize() => Size = int.MaxValue;
}
