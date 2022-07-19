namespace FocusOn.Framework.Business.Contract.Http;
[AttributeUsage(AttributeTargets.Parameter)]
public class HttpParameterAttribute : Attribute
{
    public HttpParameterAttribute(HttpParameterType type = HttpParameterType.FromRoute, string? name = default)
    {
        Type = type;
        Name = name;
    }

    public HttpParameterType Type { get; }
    public string? Name { get; }
}


public enum HttpParameterType
{
    FromRoute,
    FromBody,
    FromQuery,
    FromHeader
}