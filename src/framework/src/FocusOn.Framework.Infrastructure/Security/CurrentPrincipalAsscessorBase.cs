using System.Security.Claims;

namespace FocusOn.Framework.Security;
/// <summary>
/// 表示当前主体访问器的基类。这是一个抽象类。
/// </summary>
public abstract class CurrentPrincipalAsscessorBase : ICurrentPrincipalAccessor
{
    private readonly AsyncLocal<ClaimsPrincipal> _localPrincipal = new();

    /// <summary>
    /// 获取声明主体。
    /// </summary>
    public ClaimsPrincipal CurrentPrincipal => _localPrincipal.Value ?? GetClaimsPrincipal();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="principal">要更换的主体。</param>
    /// <returns></returns>
    public virtual IDisposable Change(ClaimsPrincipal principal)
    {
        var parent = CurrentPrincipal;
        _localPrincipal.Value = principal;
        return Disposing.Perform(() =>
        {
            _localPrincipal.Value = parent;
        });
    }

    /// <summary>
    /// 重写以获得声明主体。
    /// </summary>
    protected abstract ClaimsPrincipal GetClaimsPrincipal();


}
