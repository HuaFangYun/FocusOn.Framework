namespace FocusOn.Framework.Business.Contract.Http;
public class PutAttribute : HttpMethodAttribute
{
    public PutAttribute(string? template = default) : base(HttpMethod.Put, template)
    {
    }
}
