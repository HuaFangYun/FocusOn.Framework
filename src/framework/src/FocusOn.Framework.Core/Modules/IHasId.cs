namespace FocusOn.Framework.Modules;

/// <summary>
/// 提供具备 Id 功能的对象。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
public interface IHasId<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 获取或设置 Id。
    /// </summary>
    public TKey Id { get; set; }
}
