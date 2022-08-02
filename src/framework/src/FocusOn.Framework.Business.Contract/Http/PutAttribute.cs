namespace FocusOn.Framework.Business.Contract.Http;
/// <summary>
/// 定义具备 PUT 行为的 HTTP 请求方式。
/// </summary>
public class PutAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="PutAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public PutAttribute(string? template = default) : base(HttpMethod.Put, template)
    {
    }
}
