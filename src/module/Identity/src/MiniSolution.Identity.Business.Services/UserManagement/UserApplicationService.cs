using Microsoft.EntityFrameworkCore;

using MiniSolution.Business.Contracts.DTO;
using MiniSolution.Business.Services;
using MiniSolution.Identity.Business.Contracts.UserManagement;
using MiniSolution.Identity.Business.Contracts.UserManagement.DTO;
using MiniSolution.Identity.Business.Services.Localizations;
using MiniSolution.Identity.Business.Services.UserManagement.Entities;

namespace MiniSolution.Identity.Business.Services.UserManagement;

public class UserApplicationService<TContext, TUser, TKey> : UserApplicationService<TContext, TUser, TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>, IUserApplicationService<TKey>
    where TContext : DbContext
        where TUser : User<TKey>
{
    public UserApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public class UserApplicationService<TContext, TUser, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    : EfCoreCrudApplicationServiceBase<TContext, TUser, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>, IUserApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
        where TContext : DbContext
        where TUser : User<TKey>
        where TGetListInput : UserGetListInputDto
        where TGetListOutput : UserGetListOutputDto<TKey>
        where TGetOutput : UserGetOutputDto<TKey>
        where TCreateInput : UserCreateInputDto
        where TUpdateInput : UserUpdateInputDto
{
    public UserApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
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
