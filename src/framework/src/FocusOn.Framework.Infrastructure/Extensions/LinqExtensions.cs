using System.Linq.Expressions;

namespace FocusOn;

/// <summary>
/// LINQ 扩展。
/// </summary>
public static class LinqExtensions
{
    /// <summary>
    /// 指定条件为 <c>true</c> 时，执行 <paramref name="predicate"/> 条件筛选委托。
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="condition">条件满足。</param>
    /// <param name="predicate"><paramref name="condition"/> 是 <c>true</c> 时要执行的委托。</param>
    /// <returns></returns>
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate) => condition ? source.Where(predicate) : source;
}
