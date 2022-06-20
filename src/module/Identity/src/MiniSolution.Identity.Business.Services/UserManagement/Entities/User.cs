using MiniSolution.Business.Services.Entities;

namespace MiniSolution.Identity.Business.Services.UserManagement.Entities;

public class User<TKey> : EntityBase<TKey>
{
    public string UserName { get; set; }
}