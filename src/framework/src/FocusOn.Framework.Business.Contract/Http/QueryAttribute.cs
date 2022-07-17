namespace FocusOn.Framework.Business.Contract.Http;
public class QueryAttribute : HttpParameterAttribute
{
    public QueryAttribute(string? name = default) : base(name)
    {

    }
}
