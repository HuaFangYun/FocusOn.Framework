using Microsoft.EntityFrameworkCore;

using FocusOn.Business.Contracts.DTO;
using FocusOn.Business.Services;
using FocusOn.Identity.Business.Contracts.UserManagement;
using FocusOn.Identity.Business.Contracts.UserManagement.DTO;
using FocusOn.Identity.Business.Services.Localizations;
using FocusOn.Identity.Business.Services.UserManagement.Entities;

namespace FocusOn.Identity.Business.Services.UserManagement;

public class EfCoreUserBusinessService<TContext, TUser, TKey> : EfCoreUserBusinessService<TContext, TUser, TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>, IUserBusinessService<TKey>
    where TContext : DbContext
        where TUser : IdentityUser<TKey>
{
    public EfCoreUserBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public class EfCoreUserBusinessService<TContext, TUser, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    : EfCoreCrudBusinessServiceBase<TContext, TUser, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>, IUserBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
        where TContext : DbContext
        where TUser : IdentityUser<TKey>
        where TGetListInput : class
        where TGetListOutput : class
        where TGetOutput : class
        where TCreateInput : class
        where TUpdateInput : class
{
    public EfCoreUserBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public virtual async Task<OutputResult<TGetOutput?>> GetByUserNameAsync(string userName)
    {
        var entity = await Query.SingleOrDefaultAsync(m => m.UserName == userName, CancellationToken);
        if (entity is null)
        {
            return OutputResult<TGetOutput?>.Failed(Locale.Message_UserNotFound.StringFormat(userName));
        }
        var model = Mapper.Map<TUser, TGetOutput>(entity);
        return OutputResult<TGetOutput?>.Success(model);
    }
}
