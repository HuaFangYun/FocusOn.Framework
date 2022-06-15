using Boloni.Data;
using Boloni.Data.Entities;
using Boloni.Services.Localizations;
using Boloni.DataTransfers.Users;
using Boloni.Services.Abstractions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Boloni.DataTransfers;

namespace Boloni.Services.Users;
public class UserAppService : CrudApplicationServiceBase<BoloniDbContext, User, Guid,GetUserOutputDto,GetUserListOutputDto,GetUserListInputDto,CreateUserInputDto,UpdateUserInputDto>
{
    public UserAppService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected IPasswordHasher PasswordHasher => Services.GetRequiredService<IPasswordHasher>();

    /// <summary>
    /// 创建有密码的用户。
    /// </summary>
    /// <param name="model">密码用户输入模型。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public override async ValueTask<OutputResult> CreateAsync(CreateUserInputDto model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if(await Query.AnyAsync(m => m.UserName == model.UserName))
        {
            var message = string.Format(Locale.Message_User_UserNameDuplicate, model.UserName);
            Logger.LogError(message);
            return OutputResult.Failed(message);
        }

        if(!string.IsNullOrEmpty(model.Email) && await Query.AnyAsync(m => m.Email == model.Email))
        {
            var message = string.Format(Locale.Message_User_EmailDuplicate, model.Email);
            Logger.LogError(message);
            return OutputResult.Failed(message);
        }

        if (!string.IsNullOrEmpty(model.Mobile) && await Query.AnyAsync(m => m.Mobile == model.Mobile))
        {
            var message = string.Format(Locale.Message_User_MobileDuplicate, model.Mobile);
            Logger.LogError(message);
            return OutputResult.Failed(message);
        }

        var hashedPassword = PasswordHasher.HashPassword(model.Password);
        var user = Mapper.Map<CreateUserInputDto?, User>(model);
        user.SetPassword(hashedPassword);

        Set.Add(user);
        return await SaveChangesAsync();
    }
}
