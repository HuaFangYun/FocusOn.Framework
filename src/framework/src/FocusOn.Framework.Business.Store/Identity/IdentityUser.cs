using FocusOn.Framework.Modules.Identity;

namespace FocusOn.Framework.Business.Store.Identity;


/// <summary>
/// 表示用户的基本信息。
/// </summary>
/// <typeparam name="TKey">主键类型。该主键类型必须和 <see cref="IdentityRole{TKey}"/> 一致。</typeparam>
public class IdentityUser<TKey> : EntityBase<TKey>, IUser<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityUser{TKey}"/> 类的新实例。
    /// </summary>
    public IdentityUser()
    {

    }

    /// <summary>
    /// 使用用户名初始化 <see cref="IdentityUser{TKey}"/> 类的新实例。
    /// </summary>
    /// <param name="userName">用户名。</param>
    public IdentityUser(string userName)
    {
        UserName = userName;
    }
    /// <summary>
    /// 获取或设置用户名。
    /// </summary>
    public virtual string UserName { get; set; }

    /// <summary>
    /// 获取或设置当前用户拥有的角色集合。
    /// </summary>
    public virtual ICollection<IdentityRole<TKey>> Roles { get; set; } = new HashSet<IdentityRole<TKey>>();
}
