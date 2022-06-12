namespace Boloni.Data;
public abstract class EntityBase
{
    public Guid Id { get; set; } = new();

    public Guid? TenantId { get; set; }
}
