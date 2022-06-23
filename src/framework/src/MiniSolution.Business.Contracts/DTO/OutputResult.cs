﻿
using Microsoft.Extensions.Logging;

namespace MiniSolution.Business.Contracts.DTO;

[Serializable]
public record class OutputResult
{
    public OutputResult(IEnumerable<string?>? errors)
    {
        Errors = errors ?? Array.Empty<string?>();
    }

    public OutputResult() { }

    public IEnumerable<string?> Errors { get;private set; } = Array.Empty<string?>();

    public virtual bool Succeed => !Errors.Any();

    public static OutputResult Success() => new();
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
public record class OutputResult<TResult> : OutputResult
{
    public OutputResult(TResult? data, IEnumerable<string>? errors) :base(errors)
    {
        Data = data;
    }

    internal OutputResult(IEnumerable<string>? errors) : this(default,errors)
    {

    }

    public TResult? Data { get; }

    public static OutputResult<TResult> Success(TResult data) => new(data, null);
    public static new OutputResult<TResult> Failed(params string[] errors) => new(errors);
    public static new OutputResult<TResult> Failed(IEnumerable<string>? errors) => new(errors);
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
