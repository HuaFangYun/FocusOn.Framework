namespace MiniSolution.Business.Services.Entities;

public abstract class EntityBase<TKey>
{
    protected EntityBase()
    {
    }

    public virtual TKey Id { get; set; }
}
