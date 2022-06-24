namespace MiniSolution.Business.Contracts;
[AttributeUsage(AttributeTargets.Class| AttributeTargets.Interface| AttributeTargets.Method,AllowMultiple =true)]
public sealed class RemotingServiceAttribute : Attribute
{
    public RemotingServiceAttribute(string? template=default)
    {
        Template = template;
    }

    public string Template { get; }
    public bool Disabled { get; set; }
}
