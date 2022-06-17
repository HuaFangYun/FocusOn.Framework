using MiniSolution.ApplicationContracts.DTO;

namespace MiniSolution.Identity.ApplicationContracts.UserManagement.DTO;

public class UserGetOutputDto<TKey> : OutpuDtoBase<TKey>
{
    public string UserName { get; set; }
}
