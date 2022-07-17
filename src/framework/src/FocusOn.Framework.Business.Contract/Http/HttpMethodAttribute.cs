namespace FocusOn.Framework.Business.Contract.Http;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
public class HttpMethodAttribute : Attribute
{
    public HttpMethodAttribute(HttpMethod method, string? template = default)
    {
        Template = template;
        Method = method;
    }

    public string? Template { get; }
    public HttpMethod Method { get; }
}
