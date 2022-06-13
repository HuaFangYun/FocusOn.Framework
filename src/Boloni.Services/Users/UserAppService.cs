using System.Reflection.Metadata.Ecma335;

using Boloni.Data;
using Boloni.Data.Entities;
using Boloni.Services.Abstractions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Boloni.Services.Users;
public class UserAppService : CrudApplicationServiceBase<BoloniDbContext, User,Guid>
{
    public UserAppService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected IPasswordHasher PasswordHasher => Services.GetRequiredService<IPasswordHasher>();

    public async Task<ApplicationResult<Guid>> CreateAsync(User user, string password)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));
        }

        var hashedPassword= PasswordHasher.HashPassword(password);

        user.SetPassword(hashedPassword);

        Set.Add(user);
        await SaveChangesAsync();
        return ApplicationResult<Guid>.Success(user.Id);
    }

    public Task<bool> GetUserNameExists(string userName)
    {
        return Query.AnyAsync(m => m.UserName == userName);
    }
}
