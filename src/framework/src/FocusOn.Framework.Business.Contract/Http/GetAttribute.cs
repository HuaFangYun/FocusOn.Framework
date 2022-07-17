namespace FocusOn.Framework.Business.Contract.Http;
public class GetAttribute : HttpMethodAttribute
{
    public GetAttribute(string? template = default) : base(HttpMethod.Get, template)
    {
    }
}
