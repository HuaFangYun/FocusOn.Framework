namespace FocusOn.Framework.Business.Contract.Http;
/// <summary>
/// 定义具备 OPTIONS 行为的 HTTP 请求方式。
/// </summary>
public class OptionsAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="OptionsAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public OptionsAttribute(string? template = null) : base(HttpMethod.Options, template)
    {
    }
}
