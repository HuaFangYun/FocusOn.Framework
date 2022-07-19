namespace FocusOn.Framework.Business.Contract.Http;
public class BodyAttribute : HttpParameterAttribute
{
    public BodyAttribute(string? name = default) : base(HttpParameterType.FromBody, name)
    {

    }
}
