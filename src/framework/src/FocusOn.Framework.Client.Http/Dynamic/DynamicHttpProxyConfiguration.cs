namespace FocusOn.Framework.Client.Http.Dynamic;
/// <summary>
/// 表示动态 HTTP 代理的配置。
/// </summary>
public class DynamicHttpProxyConfiguration
{
    public DynamicHttpProxyConfiguration()
    {

    }
    public readonly static string Default = nameof(Default);

    public string Name { get; set; }

    public string BaseAddress { get; set; }

    public IList<Func<IServiceProvider, DelegatingHandler>> DelegatingHandlers { get; } = new List<Func<IServiceProvider, DelegatingHandler>>();

    /// <summary>
    /// 获取或设置基于 <see cref="HttpClientHandler"/> 的函数。
    /// </summary>
    public Func<HttpClientHandler>? PrimaryHandler { get; set; }
}
