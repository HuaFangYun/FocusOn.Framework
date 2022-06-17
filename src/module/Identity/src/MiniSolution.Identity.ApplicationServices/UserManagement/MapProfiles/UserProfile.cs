
using MiniSolution.ApplicationServices;
using MiniSolution.Identity.ApplicationContracts.UserManagement.DTO;
using MiniSolution.Identity.ApplicationServices.UserManagement.Entities;

namespace MiniSolution.Identity.ApplicationServices.UserManagement.MapProfiles;

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
