namespace Boloni.Services.Users;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword) => HashPassword(password).Equals(hashedPassword);
}
