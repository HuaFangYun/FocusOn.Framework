namespace MiniSolution.HttpApi.Client;

public interface IClientProxy
{
    IHttpClientFactory HttpClientFactory { get; }
}
