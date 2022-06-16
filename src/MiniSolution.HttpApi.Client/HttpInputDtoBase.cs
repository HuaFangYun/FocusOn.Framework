using System.Text.Json.Serialization;

namespace MiniSolution.Client.Http;

public abstract class HttpInputDtoBase
{
    protected HttpInputDtoBase(string relativePath)
    {
        RelativePath = relativePath;
    }

    [JsonIgnore]
    public string RelativePath { get; }
}
