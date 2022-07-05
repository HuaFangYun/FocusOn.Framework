namespace FocusOn.Framework.Business.Store.Identity;


/// <summary>
/// 表示角色的基本信息。
/// </summary>
/// <typeparam name="TKey">主键类型。该主键类型必须和 <see cref="IdentityUser{TKey}"/> 一致。</typeparam>
public class IdentityRole<TKey> : EntityBase<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityRole{TKey}"/> 类的新实例。
    /// </summary>
    public IdentityRole()
    {
    }

    /// <summary>
    /// 使用角色名初始化 <see cref="IdentityRole{TKey}"/> 类的新实例。
    /// </summary>
    /// <param name="name">角色名称。</param>

    public IdentityRole(string name)
    {
        Name = name;
    }
    /// <summary>
    /// 获取或设置角色名称。
    /// </summary>
    public virtual string Name { get; set; }
}
