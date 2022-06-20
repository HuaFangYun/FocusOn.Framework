using MiniSolution.Business.Contracts.DTO;

namespace MiniSolution.Identity.Business.Contracts.UserManagement.DTO;

public class UserGetOutputDto<TKey> : OutpuDtoBase<TKey>
{
    public string UserName { get; set; }
}
