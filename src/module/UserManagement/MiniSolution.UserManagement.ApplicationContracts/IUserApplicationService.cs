using MiniSolution.ApplicationContracts;
using MiniSolution.UserManagement.ApplicationContracts.DTO;

namespace MiniSolution.UserManagement.ApplicationContracts;

public interface IUserApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput> : ICrudApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TGetListInput : UserGetListInputDto
    where TGetListOutput : UserGetListOutputDto<TKey>
    where TGetOutput : UserGetOutputDto<TKey>
    where TCreateInput : UserCreateInputDto
    where TUpdateInput : UserUpdateInputDto
{
    Task<TGetOutput> GetByUserNameAsync(string userName);
}