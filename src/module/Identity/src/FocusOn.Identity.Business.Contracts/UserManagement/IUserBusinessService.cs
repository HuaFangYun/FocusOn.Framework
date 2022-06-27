using FocusOn.Business.Contracts;
using FocusOn.Business.Contracts.DTO;
using FocusOn.Identity.Business.Contracts.UserManagement.DTO;

namespace FocusOn.Identity.Business.Contracts.UserManagement;
public interface IUserBusinessService<TKey>: IUserBusinessService<TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>
{

}


public interface IUserBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput> : ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    Task<OutputResult<TGetOutput?>> GetByUserNameAsync(string userName);
}