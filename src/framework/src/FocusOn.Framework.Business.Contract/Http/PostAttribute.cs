namespace FocusOn.Framework.Business.Contract.Http;
/// <summary>
/// 定义具备 POST 行为的 HTTP 请求方式。
/// </summary>
public class PostAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="PostAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public PostAttribute(string? template = default) : base(HttpMethod.Post, template)
    {
    }
}
