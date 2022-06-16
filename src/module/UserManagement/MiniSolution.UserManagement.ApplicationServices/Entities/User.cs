using MiniSolution.ApplicationServices.Entities;

namespace MiniSolution.UserManagement.ApplicationServices.Entities;

public class User<TKey>:EntityBase<TKey>
{
    public string UserName { get; set; }
}