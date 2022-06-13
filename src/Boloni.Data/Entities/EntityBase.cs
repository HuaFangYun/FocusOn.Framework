namespace Boloni.Data.Entities;
public abstract class EntityBase<TKey>
{
    public virtual TKey Id { get; set; }
}

public abstract class EntityBase : EntityBase<Guid>
{
    public EntityBase()
    {
        Id= Guid.NewGuid();
    }
}