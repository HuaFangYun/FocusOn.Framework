namespace FocusOn.Framework.Business.Contract.Http;
public class PatchAttribute : HttpMethodAttribute
{
    public PatchAttribute(string? template = null) : base(HttpMethod.Patch, template)
    {
    }
}
