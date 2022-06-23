namespace MiniSolution.Business.Contracts.DTO;

public abstract class OutputDtoBase<TKey>
{
    public TKey Id { get; set; }
}
