using Boloni.Data;

namespace Boloni.Services;
public interface IApplicationService<TEntity> where TEntity : EntityBase
{
    ValueTask<TEntity> CreateAsync(TEntity entity);
    ValueTask<TEntity> UpdateAsync(TEntity entity);
    ValueTask<TEntity> UpdateAsync(Guid id, Action<TEntity> updateAction);
    ValueTask DeleteAsync(Guid id);
    ValueTask DeleteAsync(TEntity entity);
    ValueTask<TEntity> FindAsync(Guid id);
    Task<IReadOnlyList<TEntity>> FindListAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> predicate = default);
    Task<IReadOnlyList<TEntity>> FindPagedListAsync(int page, int size, Func<IQueryable<TEntity>, IQueryable<TEntity>> predicate = default);
}
