namespace FocusOn.Framework.Business.Contract.Http;

/// <summary>
/// 定义参数的访问方式。
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public class HttpParameterAttribute : Attribute
{
    /// <summary>
    /// 初始化 <see cref="HttpParameterAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="type">参数访问类型。</param>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public HttpParameterAttribute(HttpParameterType type = HttpParameterType.FromRoute, string? name = default)
    {
        Type = type;
        Name = name;
    }
    /// <summary>
    /// 获取参数访问类型。
    /// </summary>
    public HttpParameterType Type { get; }
    /// <summary>
    /// 获取参数名称。
    /// </summary>
    public string? Name { get; }
}

/// <summary>
/// HTTP 参数的类型。
/// </summary>
public enum HttpParameterType
{
    /// <summary>
    /// 从路由获取参数。这是默认的。
    /// </summary>
    FromRoute = 0,
    /// <summary>
    /// 从 Body 中获取参数的值。
    /// </summary>
    FromBody,
    /// <summary>
    /// 从查询字符串中获取参数的值。
    /// </summary>
    FromQuery,
    /// <summary>
    /// 从 Header 中获取参数的值。
    /// </summary>
    FromHeader
}