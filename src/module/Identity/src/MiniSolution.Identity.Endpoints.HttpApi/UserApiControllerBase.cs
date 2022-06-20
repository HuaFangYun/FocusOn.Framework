using Microsoft.AspNetCore.Mvc;

using MiniSolution.Business.Contracts.DTO;
using MiniSolution.Endpoints.HttpApi.Controllers;
using MiniSolution.Identity.Business.Contracts.UserManagement;
using MiniSolution.Identity.Business.Contracts.UserManagement.DTO;

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