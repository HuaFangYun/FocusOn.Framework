namespace FocusOn.Framework.Business.Contract.Http;
public class TraceAttribute : HttpMethodAttribute
{
    public TraceAttribute(string? template = null) : base(HttpMethod.Trace, template)
    {
    }
}
