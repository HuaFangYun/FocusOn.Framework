namespace MiniSolution.Core.MuiltyTenancy;

/// <summary>
/// 提供支持多租户的功能。
/// </summary>
public interface IHasMultiTenancy
{
    /// <summary>
    /// 获取或设置租户 Id。
    /// </summary>
    public Guid? TenantId { get; set; }
}
