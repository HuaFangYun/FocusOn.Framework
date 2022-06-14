using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Boloni.Data.Entities;
using AutoMapper;
using Boloni.Services.Abstractions.Models;
using Boloni.Services.Localizations;
using Boloni.Data.Migrations;

namespace Boloni.Services.Abstractions;
public abstract class CrudApplicationServiceBase<TContext, TEntity, TKey>
    : CrudApplicationServiceBase<TContext, TEntity, TKey, TEntity, TEntity, TEntity>, ICrudApplicationService<TEntity, TKey>
    where TContext : DbContext
    where TEntity : EntityBase<TKey>
{
    public CrudApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public abstract class CrudApplicationServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput>
    : CrudApplicationServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TEntity>, ICrudApplicationService<TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TEntity>
    where TContext : DbContext
    where TEntity : EntityBase<TKey>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
{
    public CrudApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public abstract class CrudApplicationServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    : CrudApplicationServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, ICrudApplicationService<TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    where TContext : DbContext
    where TEntity : EntityBase<TKey>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateOrUpdateInput : class
{
    public CrudApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public abstract class CrudApplicationServiceBase<TContext, TEntity,TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput> 
    :ApplicationServiceBase, ICrudApplicationService<TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TContext : DbContext
    where TEntity : EntityBase<TKey> 
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    public CrudApplicationServiceBase(IServiceProvider serviceProvider):base(serviceProvider)
    {
    }



    protected IMapper Mapper => Services.GetRequiredService<IMapper>();

    protected CancellationToken CancellationToken
    {
        get
        {
            var tokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));
            return tokenSource.Token;
        }
    }

    protected TContext Context => Services.GetRequiredService<TContext>();

    protected DbSet<TEntity> Set => Context.Set<TEntity>();

    protected IQueryable<TEntity> Query => Set.AsNoTracking();

    public virtual async ValueTask<ApplicationResult> CreateAsync(TCreateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        
        var entity = Mapper.Map<TCreateInput, TEntity>(model);
        Set.Add(entity);
        return await SaveChangesAsync();
    }

    public virtual async ValueTask<ApplicationResult> DeleteAsync(TKey id)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            return ApplicationResult.Failed(string.Format(Locale.Message_EntityNotFound, id));
        }
        Set.Remove(entity);
        return await SaveChangesAsync();
    }

    public async ValueTask<ApplicationResult> UpdateAsync(TKey id, TUpdateInput model)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            return ApplicationResult.Failed(string.Format(Locale.Message_EntityNotFound, id));
        }

        Mapper.Map(model, entity);
        return await SaveChangesAsync();
    }

    public virtual async ValueTask<TGetOutput?> GetAsync(TKey id)
    {
        var entity = await FindAsync(id);
        if (entity is not null)
        {
            return Mapper.Map<TEntity?, TGetOutput>(entity);
        }
        return default;
    }

    public virtual async Task<IReadOnlyList<TGetListOutput>> GetListAsync(TGetListInput model)
    {
        var query = Query;

        query = CreateQuery(query);

        if (model is QueryableModel<TEntity> queryable)
        {
            query = queryable.Query(query);
        }
        return  await  Mapper.ProjectTo<TGetListOutput>(query).ToListAsync(CancellationToken);
    }

    public virtual async Task<(IReadOnlyList<TGetListOutput> Data, long Total)> GetPagedListAsync(int page, int size, TGetListInput model)
    {
        var query = Query;

        query = CreateQuery(query);

        if (model is QueryableModel<TEntity> queryable)
        {
            query = queryable.Query(query);
        }

        query = query.Skip((page - 1) * size).Take(page * size);
        var data= await Mapper.ProjectTo<TGetListOutput>(query).ToListAsync(CancellationToken);

        var total = await query.CountAsync(CancellationToken);
        return (data, total);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="throwIfError"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected ValueTask<TEntity?> FindAsync(TKey id) => Set.FindAsync(id);

    protected virtual IQueryable<TEntity> CreateQuery(IQueryable<TEntity> source)
    {
        return source;
    }



    /// <summary>
    /// 保存数据库。
    /// </summary>
    /// <returns></returns>
    protected virtual async Task<ApplicationResult> SaveChangesAsync()
    {
        try
        {
            var rows = await Context.SaveChangesAsync(CancellationToken);
            if (rows > 0)
            {
                Logger.LogInformation("Save changes successfully");
                return ApplicationResult.Success();
            }
            Logger.LogWarning("Save changes failed because affected row is 0");
            return ApplicationResult.Failed("Save changes failed");
        }
        catch (AggregateException ex)
        {
            Logger.LogError(ex, string.Join(";", ex.InnerExceptions.Select(m => m.Message)));
            return ApplicationResult.Failed("Exceptions occured when saving changes, see log for details");
        }
    }
}
