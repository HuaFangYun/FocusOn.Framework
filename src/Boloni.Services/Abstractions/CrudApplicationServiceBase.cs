using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Boloni.Data.Entities;
using AutoMapper;
using Boloni.Services.Abstractions.Models;

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

    public async ValueTask<ApplicationResult> CreateAsync(TCreateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var entity = MapToEntity(model);
        Set.Add(entity);
        await SaveChangesAsync();

        return ApplicationResult.Success();
    }

    protected virtual Task<int> SaveChangesAsync()
    {
        return Context.SaveChangesAsync(CancellationToken);
    }

    public async ValueTask<ApplicationResult> DeleteAsync(TKey id)
    {
        var entity = await FindAsync(id);
        Set.Remove(entity);
        await SaveChangesAsync();
        return ApplicationResult.Success();
    }

    public async ValueTask<TGetOutput> GetAsync(TKey id)
    {
        var entity = await FindAsync(id);
        return Mapper.Map<TEntity, TGetOutput>(entity);
    }

    public async Task<IReadOnlyList<TGetListOutput>> GetListAsync(TGetListInput model)
    {
        var query = Query;

        query = CreateQuery(query);

        if (model is QueryableModel<TEntity> queryable)
        {
            query = queryable.Query(query);
        }
        return  await  Mapper.ProjectTo<TGetListOutput>(query).ToListAsync(CancellationToken);
    }

    public async Task<(IReadOnlyList<TGetListOutput> Data, long Total)> GetPagedListAsync(int page, int size, TGetListInput model)
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

    public async ValueTask<ApplicationResult> UpdateAsync(TKey id, TUpdateInput model)
    {
        try
        {
            var entity = await FindAsync(id);

            MapToEntity(model, entity);
            await SaveChangesAsync();

            return ApplicationResult.Success();
        }catch(Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return ApplicationResult.Failed(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="throwIfError"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected async ValueTask<TEntity?> FindAsync(TKey id, bool throwIfError = true)
    {
        var entity = await Set.FindAsync(id);

        if (throwIfError && entity is null)
        {
            throw new InvalidOperationException($"Entity with '{id}' not found");
        }
        return entity;
    }

    protected virtual IQueryable<TEntity> CreateQuery(IQueryable<TEntity> source)
    {
        return source;
    }

    protected virtual TEntity MapToEntity(TCreateInput input)
    {
        return Mapper.Map<TCreateInput, TEntity>(input);
    }

    protected virtual TEntity MapToEntity(TUpdateInput input,TEntity entity)
    {
        return Mapper.Map(input, entity);
    }

}
