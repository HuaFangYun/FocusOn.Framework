namespace FocusOn.Framework.Business.Contract.Http;

/// <summary>
/// 定义具备 DELETE 行为的 HTTP 请求方式。
/// </summary>
public class DeleteAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="DeleteAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public DeleteAttribute(string? template = default) : base(HttpMethod.Delete, template)
    {
    }
}
