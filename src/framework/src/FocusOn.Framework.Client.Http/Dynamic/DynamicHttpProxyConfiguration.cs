﻿namespace FocusOn.Framework.Client.Http.Dynamic;
/// <summary>
/// 表示动态 HTTP 代理的配置。
/// </summary>
public class DynamicHttpProxyConfiguration
{
    /// <summary>
    /// 默认名称。
    /// </summary>
    public readonly static string Default = nameof(Default);

    /// <summary>
    /// 初始化 <see cref="DynamicHttpProxyConfiguration"/> 类的新实例。
    /// </summary>
    public DynamicHttpProxyConfiguration()
    {

    }

    /// <summary>
    /// 获取或设置 HTTP 代理的名称。
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 获取或设置 HTTP 代理的访问地址。
    /// </summary>
    public string BaseAddress { get; set; }

    /// <summary>
    /// 用于定义每一次 HTTP 请求的一个委托处理集合。
    /// </summary>
    public IList<Func<IServiceProvider, DelegatingHandler>> DelegatingHandlers { get; } = new List<Func<IServiceProvider, DelegatingHandler>>();

    /// <summary>
    /// 获取或设置基于 <see cref="HttpClientHandler"/> 的函数。
    /// </summary>
    public Func<HttpClientHandler>? PrimaryHandler { get; set; }
}
