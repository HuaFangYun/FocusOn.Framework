
using FocusOn.Business.Services;
using FocusOn.Identity.Business.Contracts.UserManagement.DTO;
using FocusOn.Identity.Business.Services.UserManagement.Entities;

namespace FocusOn.Identity.Business.Services.UserManagement.MapProfiles;

public abstract class UserMapProfile<TUser, TKey>:UserMapProfile<TUser, TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserCreateInputDto, UserUpdateInputDto>
    where TUser : IdentityUser<TKey>
{

}

public abstract class UserMapProfile<TUser,TKey, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto> : CrudMapProfile<TUser, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto>
    where TUser:IdentityUser<TKey>
    where TGetListOutputDto : class
    where TGetOutputDto : class
    where TCreateInputDto : class
    where TUpdateInputDto : class
{
}
