using MiniSolution.ApplicationContracts.DTO;

namespace MiniSolution.UserManagement.ApplicationContracts.DTO;

public class UserGetOutputDto<TKey>:OutpuDtoBase<TKey>
{
    public string UserName { get; set; }
}
