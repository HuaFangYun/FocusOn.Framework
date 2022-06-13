using Boloni.Data.Entities.Abstractions;

namespace Boloni.Data.Entities;
public class Role : EntityBase,IHasMultiTenancy
{
    public string Name { get; set; }

    public Guid? TenantId { get; set; }
    public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
}
