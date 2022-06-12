namespace Boloni.Data;
public class Role : EntityBase
{
    public string Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
}
