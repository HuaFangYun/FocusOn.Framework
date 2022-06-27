
using FocusOn.Business.Services;
using FocusOn.Identity.Business.Contracts.UserManagement.DTO;
using FocusOn.Identity.Business.Services.UserManagement.Entities;

namespace FocusOn.Identity.Business.Services.UserManagement.MapProfiles;

public abstract class UserMapProfile<TUser, TKey>:UserMapProfile<TUser, TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserCreateInputDto, UserUpdateInputDto>
    where TUser : User<TKey>
{

}

public abstract class UserMapProfile<TUser,TKey, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto> : CrudMapProfile<TUser, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto>
    where TUser:User<TKey>
    where TGetListOutputDto : UserGetListOutputDto<TKey>
    where TGetOutputDto : UserGetOutputDto<TKey>
    where TCreateInputDto : UserCreateInputDto
    where TUpdateInputDto : UserUpdateInputDto
{
}
