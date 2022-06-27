using Microsoft.AspNetCore.Mvc;

using FocusOn.Business.Contracts.DTO;
using FocusOn.Endpoints.HttpApi.Controllers;
using FocusOn.Identity.Business.Contracts.UserManagement;
using FocusOn.Identity.Business.Contracts.UserManagement.DTO;

namespace FocusOn.Identity.HttpApi;

public abstract class UserApiControllerBase<TUserBusinessService, TKey> :UserApiControllerBase<TUserBusinessService, TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>
    where TUserBusinessService : IUserBusinessService<TKey>
{

}

[Route("users")]
public abstract class UserApiControllerBase<TUserBusinessService, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto> : CrudApiControllerBase<TUserBusinessService, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>, IUserBusinessService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    where TUserBusinessService : IUserBusinessService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    where TGetListInputDto : class
    where TGetListOutputDto : class
    where TGetOutputDto : class
    where TCreateInputDto : class
    where TUpdateInputDto : class
{
    [HttpGet("username/{userName}")]
    public virtual Task<OutputResult<TGetOutputDto?>> GetByUserNameAsync(string userName)
    => BusinessService.GetByUserNameAsync(userName);
}