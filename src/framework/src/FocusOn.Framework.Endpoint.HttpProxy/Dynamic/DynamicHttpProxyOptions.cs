namespace FocusOn.Framework.Endpoint.HttpProxy.Dynamic;

public class DynamicHttpProxyOptions
{
    public Dictionary<Type, DynamicHttpProxyConfiguration> HttpProxies { get; set; } = new();
}
