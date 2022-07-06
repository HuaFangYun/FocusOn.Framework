using FocusOn.Framework.Modules;

namespace FocusOn.Framework.Business.Store;

/// <summary>
/// 提供一个具备 id 字段的实体基类。这是一个抽象类。
/// </summary>
/// <typeparam name="TKey"></typeparam>
public abstract class EntityBase<TKey>:IHasId<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="EntityBase{TKey}"/> 类的新实例。
    /// </summary>
    protected EntityBase()
    {
    }
    /// <summary>
    /// 获取或设置 Id。
    /// </summary>
    public virtual TKey Id { get; set; }
}