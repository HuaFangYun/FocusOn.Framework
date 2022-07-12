namespace FocusOn.Framework.Modules;

/// <summary>
/// 提供具备创建时间的功能。
/// </summary>
public interface IHasCreateTime
{
    /// <summary>
    /// 获取创建时间。
    /// </summary>
    public DateTime? CreateTime { get; }
}
