namespace MiniSolution.ApplicationContracts.DTO;

public class PagedOutputDto<TItem> where TItem : class
{
    public PagedOutputDto(IReadOnlyList<TItem> items,long total)
    {
        Items = items;
        Total = total;
    }

    public IReadOnlyList<TItem> Items { get; }
    public long Total { get; }
}
