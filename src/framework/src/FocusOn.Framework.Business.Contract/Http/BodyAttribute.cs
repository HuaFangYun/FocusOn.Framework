namespace FocusOn.Framework.Business.Contract.Http;

/// <summary>
/// 要求参数必须是使用 Body 的形式访问。
/// </summary>
public class BodyAttribute : HttpParameterAttribute
{
    /// <summary>
    /// 初始化 <see cref="BodyAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public BodyAttribute(string? name = default) : base(HttpParameterType.FromBody, name)
    {

    }
}
