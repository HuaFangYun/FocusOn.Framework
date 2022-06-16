namespace MiniSolution.ApplicationServices.Entities;

public abstract class EntityBase<TKey>
{
    protected EntityBase()
    {
    }

    public virtual TKey Id { get; set; }
}
