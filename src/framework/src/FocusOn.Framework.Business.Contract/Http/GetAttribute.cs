namespace FocusOn.Framework.Business.Contract.Http;
/// <summary>
/// 定义具备 GET 行为的 HTTP 请求方式。
/// </summary>
public class GetAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="GetAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public GetAttribute(string? template = default) : base(HttpMethod.Get, template)
    {
    }
}
