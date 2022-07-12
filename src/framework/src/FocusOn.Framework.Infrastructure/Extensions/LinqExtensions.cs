using System.Linq.Expressions;

namespace FocusOn;

/// <summary>
/// LINQ 扩展。
/// </summary>
public static class LinqExtensions
{
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition,Expression<Func<TSource,bool>> predicate)
    {
        if (condition)
        {
            return source.Where(predicate);
        }
        return source;
    }
}
