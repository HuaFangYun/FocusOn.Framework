using FocusOn.Business.Services.Entities;

namespace FocusOn.Identity.Business.Services.UserManagement.Entities;

public class User<TKey> : EntityBase<TKey>
{
    public string UserName { get; set; }
}