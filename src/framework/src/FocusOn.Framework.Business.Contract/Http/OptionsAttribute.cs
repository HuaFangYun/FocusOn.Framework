namespace FocusOn.Framework.Business.Contract.Http;
internal class OptionsAttribute : HttpMethodAttribute
{
    public OptionsAttribute(string? template = null) : base(HttpMethod.Options, template)
    {
    }
}
