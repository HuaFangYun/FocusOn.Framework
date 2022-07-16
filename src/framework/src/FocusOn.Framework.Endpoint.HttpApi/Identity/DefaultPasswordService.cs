using System.Security.Cryptography;

namespace FocusOn.Framework.Endpoint.HttpApi.Identity;

/// <summary>
/// 默认的密码服务。
/// </summary>
internal class DefaultPasswordService : IHashPasswordService
{
    /// <summary>
    /// 使用 MD5 算法对密码进行哈希运算。
    /// </summary>
    /// <param name="password">要运算的密码字符串。</param>
    /// <returns>经过 MD5 算法哈希运算后的密码字符串。</returns>
    public virtual string Hash(string password) => Convert.ToBase64String(MD5.Create().ComputeHash(password.GetBytes()));
}
