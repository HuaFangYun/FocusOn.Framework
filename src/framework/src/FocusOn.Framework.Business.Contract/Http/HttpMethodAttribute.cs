namespace FocusOn.Framework.Business.Contract.Http;

/// <summary>
/// 定义 HTTP 的请求方式的特性。
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
public class HttpMethodAttribute : Attribute
{
    /// <summary>
    /// 初始化 <see cref="HttpMethodAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="method">请求方式。</param>
    /// <param name="template">路由模板。</param>
    public HttpMethodAttribute(HttpMethod method, string? template = default)
    {
        Template = template;
        Method = method;
    }
    /// <summary>
    /// 获取路由模板。
    /// </summary>
    public string? Template { get; }
    /// <summary>
    /// 获取请求方式。
    /// </summary>
    public HttpMethod Method { get; }
}
