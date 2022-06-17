using MiniSolution.ApplicationServices.Entities;

namespace MiniSolution.Identity.ApplicationServices.UserManagement.Entities;

public class User<TKey> : EntityBase<TKey>
{
    public string UserName { get; set; }
}