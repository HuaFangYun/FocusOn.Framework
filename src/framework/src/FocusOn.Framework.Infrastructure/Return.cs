using Microsoft.Extensions.Logging;

namespace FocusOn;

/// <summary>
/// 表示输出返回结果。
/// </summary>
[Serializable]
public record class Return
{
    /// <summary>
    /// 初始化 <see cref="Return"/> 类的新实例。
    /// </summary>
    /// <param name="errors">错误集合。</param>
    public Return(IEnumerable<string?>? errors)
    {
        Errors = errors ?? Array.Empty<string?>();
    }


    /// <summary>
    /// 获取返回的错误信息数组。
    /// </summary>
    public IEnumerable<string?> Errors { get; private set; } = Array.Empty<string?>();

    /// <summary>
    /// 获取一个布尔值，表示返回结果是否成功。
    /// </summary>
    public virtual bool Succeed => !Errors.Any();

    /// <summary>
    /// 表示操作结果是成功的。
    /// </summary>
    public static Return Success() => new(Array.Empty<string?>());
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static Return Failed(params string[] errors) => new(errors);
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static Return Failed(IEnumerable<string?>? errors) => new(errors);

    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/> 实例。</param>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static Return Failed(ILogger? logger, params string[] errors)
    {
        logger?.LogError(errors.JoinString(";"));
        return Failed(errors);
    }
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/> 实例。</param>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static new Return Failed(ILogger? logger, IEnumerable<string> errors)
        => Failed(logger, errors.ToArray());
    /// <summary>
    /// 表示操作结果是失败的，并记录异常日志。
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/> 实例。</param>
    /// <param name="ex">要记录的异常。</param>
    public static Return Failed(ILogger? logger, Exception ex)
    {
        logger?.LogError(ex, ex.Message);
        return new(new[] { ex.Message });
    }
}

/// <summary>
/// 表示具有返回值 <typeparamref name="TResult"/> 的输出返回结果。
/// </summary>
/// <typeparam name="TResult">返回值的类型。</typeparam>
[Serializable]
public record class Return<TResult> : Return
{
    /// <summary>
    /// 初始化 <see cref="Return{TResult}"/> 类的新实例。
    /// </summary>
    /// <param name="data">要返回的数据。</param>
    /// <param name="errors">错误信息数组。</param>
    public Return(TResult? data, IEnumerable<string>? errors) : base(errors) => Data = data;

    /// <summary>
    /// 获取执行结果成功后的返回数据。
    /// </summary>
    public TResult? Data { get; }

    /// <summary>
    /// 表示操作结果是成功的，并设置返回的数据。
    /// </summary>
    public static Return<TResult?> Success(TResult? data) => new(data, null);
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static new Return<TResult> Failed(params string[] errors) => new(default, errors);
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static new Return<TResult> Failed(IEnumerable<string>? errors) => new(default, errors);
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/> 实例。</param>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static new Return<TResult> Failed(ILogger? logger, params string[] errors)
    {
        if (errors is null)
        {
            throw new ArgumentNullException(nameof(errors));
        }

        logger?.LogError(errors.JoinString(";"));
        return Failed(errors);
    }
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/> 实例。</param>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static new Return<TResult> Failed(ILogger? logger, IEnumerable<string> errors)
        => Failed(logger, errors.ToArray());

    /// <summary>
    /// 表示操作结果是失败的，并记录异常日志。
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/> 实例。</param>
    /// <param name="ex">要记录的异常。</param>
    public static new Return<TResult> Failed(ILogger? logger, Exception ex)
    {
        logger?.LogError(ex, ex.Message);
        return Failed(ex.Message);
    }
}
