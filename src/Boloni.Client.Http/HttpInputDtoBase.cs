using System.Text.Json.Serialization;

namespace Boloni.Client.Http;

public abstract class HttpInputDtoBase
{
    protected HttpInputDtoBase(string relativePath)
    {
        RelativePath = relativePath;
    }

    [JsonIgnore]
    public string RelativePath { get; }
}
