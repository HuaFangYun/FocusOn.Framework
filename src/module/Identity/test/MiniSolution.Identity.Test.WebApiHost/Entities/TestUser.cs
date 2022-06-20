using MiniSolution.Identity.Business.Services.UserManagement.Entities;

namespace MiniSolution.Identity.Test.WebApiHost.Entities
{
    public class TestUser:User<Guid>
    {
        public DateTime? BirthDay { get; set; }
    }
}
