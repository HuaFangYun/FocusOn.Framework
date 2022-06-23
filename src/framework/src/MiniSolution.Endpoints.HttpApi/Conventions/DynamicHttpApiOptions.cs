namespace MiniSolution.Endpoints.HttpApi.Conventions;

public class DynamicHttpApiOptions
{
    public string RootPrefix { get; set; } = "api";

    public IList<string> ReplaceNames { get;set; } = new List<string>()
    {
        
    };
}
