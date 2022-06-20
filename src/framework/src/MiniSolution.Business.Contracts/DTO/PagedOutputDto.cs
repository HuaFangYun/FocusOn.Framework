using System.Text.Json.Serialization;

namespace MiniSolution.Business.Contracts.DTO;

public record class PagedOutputDto<TItem> where TItem : class
{
    [JsonConstructor]
    public PagedOutputDto(IReadOnlyList<TItem> items,long total)
    {
        Items = items;
        Total = total;
    }

    public IReadOnlyList<TItem> Items { get; }
    public long Total { get; }
}
