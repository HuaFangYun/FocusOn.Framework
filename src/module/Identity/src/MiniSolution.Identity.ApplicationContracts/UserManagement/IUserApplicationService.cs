using MiniSolution.ApplicationContracts;
using MiniSolution.ApplicationContracts.DTO;
using MiniSolution.Identity.ApplicationContracts.UserManagement.DTO;

namespace MiniSolution.Identity.ApplicationContracts.UserManagement;
public interface IUserApplicationService<TKey>: IUserApplicationService<TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>
{

}
public interface IUserApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput> : ICrudApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TGetListInput : UserGetListInputDto
    where TGetListOutput : UserGetListOutputDto<TKey>
    where TGetOutput : UserGetOutputDto<TKey>
    where TCreateInput : UserCreateInputDto
    where TUpdateInput : UserUpdateInputDto
{
    Task<OutputResult<TGetOutput?>> GetByUserNameAsync(string userName);
}