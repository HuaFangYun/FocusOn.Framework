namespace FocusOn.Framework.Business.Contract.Http;
[AttributeUsage(AttributeTargets.Parameter)]
public class HttpParameterAttribute : Attribute
{
    public HttpParameterAttribute(string? name = default)
    {
        Name = name;
    }

    public string? Name { get; }
}
