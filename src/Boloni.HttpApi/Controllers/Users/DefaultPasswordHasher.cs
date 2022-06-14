using System.Security.Cryptography;
using System.Text;

namespace Boloni.HttpApi.Users;

public class DefaultPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));
        }

        var md5 = MD5.Create();

        var hashedBuffer= md5.ComputeHash(Encoding.Default.GetBytes(password));

        return Convert.ToBase64String(hashedBuffer);
    }
}
