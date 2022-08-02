namespace FocusOn.Framework.Business.Contract.Http;
/// <summary>
/// 定义具备 PATCH 行为的 HTTP 请求方式。
/// </summary>
public class PatchAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="PatchAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public PatchAttribute(string? template = null) : base(HttpMethod.Patch, template)
    {
    }
}
