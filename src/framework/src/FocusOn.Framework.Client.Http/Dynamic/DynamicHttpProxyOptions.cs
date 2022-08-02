namespace FocusOn.Framework.Client.Http.Dynamic;

public class DynamicHttpProxyOptions
{
    public Dictionary<Type, DynamicHttpProxyConfiguration> HttpProxies { get; set; } = new();
}
