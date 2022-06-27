using FocusOn.Identity.Business.Services.UserManagement.Entities;

namespace FocusOn.Identity.Test.WebApiHost.Entities
{
    public class TestUser:IdentityUser<Guid>
    {
        public DateTime? BirthDay { get; set; }
    }
}
