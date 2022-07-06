namespace FocusOn.Framework.Modules.Identity;

/// <summary>
/// 提供用户的基本标识信息。
/// </summary>
/// <typeparam name="TKey">标识类型。</typeparam>
public interface IUser<TKey>:IHasId<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 获取用户名。
    /// </summary>
    public string UserName { get; }
}