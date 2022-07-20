using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace FocusOn.Framework.Business.Contract;
public sealed class Return
{
    internal Return(params string[] errors)
    {
        Errors = errors ?? Array.Empty<string>();
    }

    public IEnumerable<string> Errors { get; private set; }

    public bool Succeed => !Errors.Any();
    public bool Failure => !Succeed;

    public object? Data { get; init; }

    /// <summary>
    /// 表示操作结果是成功的。
    /// </summary>
    public static Return Success(object? data = default) => new() { Data = data };
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static Return Failed(params string[] errors) => new(errors);

    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/> 实例。</param>
    /// <param name="errors">操作失败的错误信息数组。</param>
    /// <exception cref="ArgumentNullException"><paramref name="errors"/> 是 null。</exception>
    public static Return Failed(ILogger? logger, params string[] errors)
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
    /// <exception cref="ArgumentNullException"><paramref name="errors"/> 是 null。</exception>
    public static Return Failed(ILogger? logger, IEnumerable<string> errors)
    {
        if (errors is null)
        {
            throw new ArgumentNullException(nameof(errors));
        }

        return Failed(logger, errors.ToArray());
    }

    /// <summary>
    /// 表示操作结果是失败的，并记录异常日志。
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/> 实例。</param>
    /// <param name="exception">要记录的异常。</param>
    /// <exception cref="ArgumentNullException"><paramref name="exception"/> 是 null。</exception>
    public static Return Failed(ILogger? logger, Exception exception)
    {
        if (exception is null)
        {
            throw new ArgumentNullException(nameof(exception));
        }

        logger?.LogError(exception, exception.Message);
        return new(new[] { exception.Message });
    }
}
