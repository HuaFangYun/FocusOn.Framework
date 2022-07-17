namespace FocusOn.Framework.Business.Contract.Http;
public class HeaderAttribute : HttpParameterAttribute
{
    public HeaderAttribute(string? name = default) : base(name)
    {

    }
}
