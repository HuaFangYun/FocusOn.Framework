using FocusOn.Business.Contracts.DTO;

namespace FocusOn.Identity.Business.Contracts.UserManagement.DTO;

public class UserGetOutputDto<TKey> : OutputDtoBase<TKey>
{
    public string UserName { get; set; }
}
