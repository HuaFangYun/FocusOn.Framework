using Microsoft.AspNetCore.Mvc;

using MiniSolution.ApplicationContracts.DTO;
using MiniSolution.HttpApi.Controllers;
using MiniSolution.Identity.ApplicationContracts.UserManagement;
using MiniSolution.Identity.ApplicationContracts.UserManagement.DTO;

namespace MiniSolution.Identity.HttpApi;

public abstract class UserApiControllerBase<TUserApplicationService, TKey> :UserApiControllerBase<TUserApplicationService, TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>
    where TUserApplicationService : IUserApplicationService<TKey>
{

}

[Route("users")]
public abstract class UserApiControllerBase<TUserApplicationService, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto> : CrudApiControllerBase<TUserApplicationService, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>, IUserApplicationService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    where TUserApplicationService : IUserApplicationService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    where TGetListInputDto : UserGetListInputDto
    where TGetListOutputDto : UserGetListOutputDto<TKey>
    where TGetOutputDto : UserGetOutputDto<TKey>
    where TCreateInputDto : UserCreateInputDto
    where TUpdateInputDto : UserUpdateInputDto
{
    [HttpGet("username/{userName}")]
    public virtual Task<OutputResult<TGetOutputDto?>> GetByUserNameAsync(string userName)
    => AppService.GetByUserNameAsync(userName);
}