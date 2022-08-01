using System.Security.Claims;

namespace FocusOn.Framework.Security;
/// <summary>
/// 表示当前主体访问器的基类。这是一个抽象类。
/// </summary>
public abstract class CurrentPrincipalAsscessorBase : ICurrentPrincipalAccessor
{
    /// <summary>
    /// 获取声明主体。
    /// </summary>
    public ClaimsPrincipal CurrentPrincipal => GetClaimsPrincipal();
    /// <summary>
    /// 重写以获得声明主体。
    /// </summary>
    protected abstract ClaimsPrincipal GetClaimsPrincipal();
}
