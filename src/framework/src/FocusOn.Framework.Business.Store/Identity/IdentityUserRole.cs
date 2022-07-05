namespace FocusOn.Framework.Business.Store.Identity;


/// <summary>
/// 表示用户和角色关联。
/// </summary>
/// <typeparam name="TKey">用户和角色的主键类型。</typeparam>
public class IdentityUserRole<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserRole{TKey}"/>
    /// </summary>
    public IdentityUserRole()
    {
    }

    /// <summary>
    /// 用户的 Id。
    /// </summary>
    public virtual TKey UserId { get; set; }

    /// <summary>
    /// 角色的 Id。
    /// </summary>
    public virtual TKey RoleId { get; set; }
}
