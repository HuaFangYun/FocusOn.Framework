namespace Boloni.Services.Abstractions.Models;

public abstract class QueryableModel<TEntity> where TEntity : class
{
    public virtual IQueryable<TEntity> Query(IQueryable<TEntity> source) => source;
}
