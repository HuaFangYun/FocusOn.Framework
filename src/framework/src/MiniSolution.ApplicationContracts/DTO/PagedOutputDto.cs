using System.Text.Json.Serialization;

namespace MiniSolution.ApplicationContracts.DTO;

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
