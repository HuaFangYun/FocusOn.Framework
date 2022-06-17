using MiniSolution.Identity.ApplicationServices.UserManagement.Entities;

namespace MiniSolution.Identity.Test.WebApiHost.Entities
{
    public class TestUser:User<Guid>
    {
        public DateTime? BirthDay { get; set; }
    }
}
