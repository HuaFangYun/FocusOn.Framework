using MiniSolution.Business.Contracts;
using MiniSolution.Business.Contracts.DTO;
using MiniSolution.Identity.Business.Contracts.UserManagement.DTO;

namespace MiniSolution.Identity.Business.Contracts.UserManagement;
public interface IUserApplicationService<TKey>: IUserApplicationService<TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>
{

}
public interface IUserApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput> : ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TGetListInput : UserGetListInputDto
    where TGetListOutput : UserGetListOutputDto<TKey>
    where TGetOutput : UserGetOutputDto<TKey>
    where TCreateInput : UserCreateInputDto
    where TUpdateInput : UserUpdateInputDto
{
    Task<OutputResult<TGetOutput?>> GetByUserNameAsync(string userName);
}