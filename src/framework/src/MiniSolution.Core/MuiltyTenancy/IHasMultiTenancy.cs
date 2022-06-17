namespace MiniSolution.Core.MuiltyTenancy;

public interface IHasMultiTenancy
{
    public Guid? TenantId { get; set; }
}
