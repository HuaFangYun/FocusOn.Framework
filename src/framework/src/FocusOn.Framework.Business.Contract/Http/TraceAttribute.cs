namespace FocusOn.Framework.Business.Contract.Http;
/// <summary>
/// 定义具备 TRACE 行为的 HTTP 请求方式。
/// </summary>
public class TraceAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="TraceAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public TraceAttribute(string? template = null) : base(HttpMethod.Trace, template)
    {
    }
}
