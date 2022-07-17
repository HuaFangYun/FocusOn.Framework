using System.Diagnostics.CodeAnalysis;

namespace FocusOn.Framework.Business.Contract.Http;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class RouteAttribute : Attribute
{
    public RouteAttribute([NotNull] string template)
    {
        Template = template;
    }

    public string Template { get; }
}
