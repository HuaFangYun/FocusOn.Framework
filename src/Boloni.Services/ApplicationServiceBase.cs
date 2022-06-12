using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Boloni.Services;
public class ApplicationServiceBase<TContext, TEntity> : IApplicationService<TEntity>
    where TContext : DbContext
    where TEntity : Boloni.Data.EntityBase
{
    public ApplicationServiceBase(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }


    protected IServiceProvider ServiceProvider { get; }

    protected ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();

    protected ILogger Logger => LoggerFactory.CreateLogger(GetType().Name);

    protected TContext Context => ServiceProvider.GetRequiredService<TContext>();

    protected DbSet<TEntity> Set => Context.Set<TEntity>();

    protected IQueryable<TEntity> Query => Set.AsNoTracking();

    public async ValueTask<TEntity> CreateAsync(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await Set.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        Set.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async ValueTask<TEntity> UpdateAsync(Guid id, Action<TEntity> updateAction)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            throw new InvalidOperationException($"Entity with id '{id}' is not found");
        }

        updateAction(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async ValueTask DeleteAsync(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        Set.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual ValueTask<TEntity?> FindAsync(Guid id)
    {
        return Set.FindAsync(id);
    }

    public virtual async Task<IReadOnlyList<TEntity>> FindListAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> predicate = default)
    {
        var query = Query;
        query = CreateQuery(query);
        if (predicate is not null)
        {
            query = predicate(query);
        }
        var result = await query.ToListAsync();
        return result;
    }

    public virtual async Task<IReadOnlyList<TEntity>> FindPagedListAsync(int page, int size, Func<IQueryable<TEntity>, IQueryable<TEntity>> predicate = default)
    {
        var query = Query;
        query = CreateQuery(query);
        if (predicate is not null)
        {
            query = predicate(query);
        }
        var result = await query.Skip((page - 1) * size).Take(size).ToListAsync();
        return result;
    }

    protected virtual IQueryable<TEntity> CreateQuery(IQueryable<TEntity> source)
    {
        return source;
    }

    public async ValueTask DeleteAsync(Guid id)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            throw new InvalidOperationException($"Entity with id '{id}' is not found");
        }
        await DeleteAsync(entity);
    }
}
