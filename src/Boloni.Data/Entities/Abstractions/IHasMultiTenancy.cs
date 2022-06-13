namespace Boloni.Data.Entities.Abstractions;

public interface IHasMultiTenancy
{
    public Guid? TenantId { get; set; }
}
