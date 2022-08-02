namespace FocusOn.Framework.Business.Contract.Http;

/// <summary>
/// 参数将从 Header 中获取值。
/// </summary>
public class HeaderAttribute : HttpParameterAttribute
{
    /// <summary>
    /// 初始化 <see cref="HeaderAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public HeaderAttribute(string? name = default) : base(HttpParameterType.FromHeader, name)
    {

    }
}
