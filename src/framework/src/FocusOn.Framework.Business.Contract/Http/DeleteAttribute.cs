namespace FocusOn.Framework.Business.Contract.Http;
public class DeleteAttribute : HttpMethodAttribute
{

    public DeleteAttribute(string? template = default) : base(HttpMethod.Delete, template)
    {
    }
}
