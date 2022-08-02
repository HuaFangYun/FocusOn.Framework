using System.Security.Claims;

namespace FocusOn.Framework.Security;

/// <summary>
/// 定义当前主体对象的访问器。
/// </summary>
public interface ICurrentPrincipalAccessor
{
    /// <summary>
    /// 获取当前的主体。
    /// </summary>
    ClaimsPrincipal CurrentPrincipal { get; }

    /// <summary>
    /// 更换主体。这是临时性的。
    /// </summary>
    /// <param name="principal">要更换的主体。</param>
    /// <returns></returns>
    IDisposable Change(ClaimsPrincipal principal);
}
