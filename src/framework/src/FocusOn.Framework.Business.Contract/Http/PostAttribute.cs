namespace FocusOn.Framework.Business.Contract.Http;
public class PostAttribute : HttpMethodAttribute
{
    public PostAttribute(string? template = default) : base(HttpMethod.Post, template)
    {
    }
}
