namespace MiniSolution.Endpoints.HttpApi.Proxy;

public interface IHttpApiClientProxy
{
    IHttpClientFactory HttpClientFactory { get; }
}
