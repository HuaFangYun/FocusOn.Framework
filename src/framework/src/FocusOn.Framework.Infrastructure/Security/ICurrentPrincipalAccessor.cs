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
}
