using MiniSolution.Business.Contracts.DTO;

namespace MiniSolution.Identity.Business.Contracts.UserManagement.DTO;

public class UserGetOutputDto<TKey> : OutputDtoBase<TKey>
{
    public string UserName { get; set; }
}
