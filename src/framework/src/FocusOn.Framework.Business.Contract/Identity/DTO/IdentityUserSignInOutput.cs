namespace FocusOn.Framework.Business.Contract.Identity.DTO;

/// <summary>
/// 用户认证登录的输出。
/// </summary>
public class IdentityUserSignInOutput
{
    /// <summary>
    /// 使用令牌初始化 <see cref="IdentityUserSignInOutput"/> 类的新实例。
    /// </summary>
    /// <param name="token">用户成功登录的令牌。</param>
    public IdentityUserSignInOutput(string token)
    {
        Token = token;
    }
    /// <summary>
    /// 获取认证过的令牌。
    /// </summary>
    public string Token { get; }

    /// <summary>
    /// 获取或设置过期时间。
    /// </summary>
    public DateTimeOffset? ExpiredAt { get; set; }
}
