namespace Boloni.DataTransfers;

public class OutputResult
{
    internal OutputResult(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public OutputResult() { }

    public IEnumerable<string> Errors { get; private set; } = Array.Empty<string>();

    public bool Succeed => !Errors.Any();

    public static OutputResult Success() => new();
    public static OutputResult Failed(params string[] errors) => new(errors);
}

public class OutputModel<TResult> : OutputResult
{
    internal OutputModel(TResult data)
    {
        Data = data;
    }

    public TResult Data { get; }

    public static OutputModel<TResult> Success(TResult data) => new OutputModel<TResult>(data);
}
