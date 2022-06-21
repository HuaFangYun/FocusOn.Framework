
using Microsoft.Extensions.Logging;

namespace MiniSolution.Business.Contracts.DTO;

/// <summary>
/// 表示输出返回结果。
/// </summary>
[Serializable]
public class OutputResult
{
    /// <summary>
    /// 初始化 <see cref="OutputResult"/> 类的新实例。
    /// </summary>
    /// <param name="errors">错误集合。</param>
    public OutputResult(IEnumerable<string?>? errors)
    {
        Errors = errors ?? Array.Empty<string?>();
    }

    public IEnumerable<string?> Errors { get;private set; } = Array.Empty<string?>();

    public virtual bool Succeed => !Errors.Any();

    public static OutputResult Success() => new (default);
    public static OutputResult Failed(params string[] errors) => new(errors);
    public static OutputResult Failed(IEnumerable<string?>? errors) => new(errors);

    public static OutputResult Failed(ILogger logger, params string[] errors)
    {
        logger.LogError(errors.Join(";"));
        return Failed(errors);
    }
    public static OutputResult Failed(ILogger logger,Exception ex)
    {
        logger.LogError(ex, ex.Message);
        return new(new[] { ex.Message });
    }
}

[Serializable]
public class OutputResult<TResult> : OutputResult
{
    public OutputResult(TResult? data, IEnumerable<string>? errors) :base(errors)
    {
        Data = data;
    }


    public TResult? Data { get; }

    public static OutputResult<TResult> Success(TResult data) => new(data, null);
    public static new OutputResult<TResult> Failed(params string[] errors) => new(default, errors);
    public static new OutputResult<TResult> Failed(IEnumerable<string>? errors) => new(default, errors);
    public static new OutputResult<TResult> Failed(ILogger logger, params string[] errors)
    {
        logger.LogError(errors.Join(";"));
        return Failed(errors);
    }
    public static new OutputResult<TResult> Failed(ILogger logger, Exception ex)
    {
        logger.LogError(ex, ex.Message);
        return Failed(ex.Message);
    }
}
