namespace FocusOn.Framework.Client.Http;

/// <summary>
/// 提供对 HTTP API 的代理客户端。
/// </summary>
public interface IHttpApiClientProxy
{
    /// <summary>
    /// 获取 <see cref="IHttpClientFactory"/> 实例。
    /// </summary>
    IHttpClientFactory HttpClientFactory { get; }
}
