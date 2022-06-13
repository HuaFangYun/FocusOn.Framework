using Boloni.Data.Entities.Abstractions;

namespace Boloni.Data.Entities;
public class User : EntityBase,IHasMultiTenancy
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 手机号
    /// </summary>
    public string? Mobile { get; set; }

    public string HashedPassword { get;private set; }

    public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();


    public Guid? TenantId { get; set; }

    public void SetPassword(string hashedPwd) => HashedPassword = hashedPwd;
}
