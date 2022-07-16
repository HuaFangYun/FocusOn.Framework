namespace FocusOn.Framework.Endpoint.HttpApi.Identity;

/// <summary>
/// 提供密码哈希的服务。
/// </summary>
public interface IHashPasswordService
{
    /// <summary>
    /// 对指定的密码进行哈希运算。
    /// </summary>
    /// <param name="password">要进行哈希运算的密码字符串。</param>
    /// <returns>哈希运算后的密码字符串。</returns>
    string Hash(string password);
    /// <summary>
    /// 校验指定密码与哈希后的密码是否一致。
    /// </summary>
    /// <param name="password">密码字符串。</param>
    /// <param name="hashedPassword">哈希运算后的密码字符串。</param>
    /// <returns>若一致则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    bool Verify(string password, string hashedPassword) => Hash(password).Equals(hashedPassword);
}
