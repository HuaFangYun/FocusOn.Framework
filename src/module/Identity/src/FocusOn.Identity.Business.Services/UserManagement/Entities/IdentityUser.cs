using FocusOn.Business.Services.Entities;

namespace FocusOn.Identity.Business.Services.UserManagement.Entities;

public class IdentityUser<TKey> : EntityBase<TKey>
{
    public string UserName { get; set; }
}