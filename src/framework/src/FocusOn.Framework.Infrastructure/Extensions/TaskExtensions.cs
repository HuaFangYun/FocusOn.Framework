namespace FocusOn.Framework;

/// <summary>
/// Task 任务扩展。
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    /// 转换成 <see cref="ValueTask"/> 类型。
    /// </summary>
    /// <param name="task">要转换的任务。</param>
    public static ValueTask ToValueTask(this Task task) => new(task);

    /// <summary>
    /// 转换成 <see cref="ValueTask{TResult}"/> 类型。
    /// </summary>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="task">要转换的任务。</param>
    public static ValueTask<TResult> ToValueTask<TResult>(this Task<TResult> task) => new(task);

    /// <summary>
    /// 转换成 <see cref="ValueTask{TResult}"/> 类型。
    /// </summary>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="result">要转换的结果。</param>
    public static ValueTask<TResult> ToValueTask<TResult>(this TResult result) => new(result);
}
