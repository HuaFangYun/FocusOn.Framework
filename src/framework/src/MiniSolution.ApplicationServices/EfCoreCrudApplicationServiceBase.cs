using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using MiniSolution.ApplicationServices.Localizations;
using MiniSolution.ApplicationContracts.DTO;
using MiniSolution.ApplicationContracts;

namespace MiniSolution.ApplicationServices;
public abstract class EfCoreCrudApplicationServiceBase<TContext, TEntity, TKey>
    : EfCoreCrudApplicationServiceBase<TContext, TEntity, TKey, TEntity, TEntity, TEntity>, ICrudApplicationService<TKey, TEntity, TEntity, TEntity, TEntity>
    where TContext : DbContext
    where TEntity : class
{
    public EfCoreCrudApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public abstract class EfCoreCrudApplicationServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput>
    : EfCoreCrudApplicationServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TEntity>, ICrudApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TEntity>
    where TContext : DbContext
    where TEntity : class
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
{
    public EfCoreCrudApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public abstract class EfCoreCrudApplicationServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    : EfCoreCrudApplicationServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, ICrudApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    where TContext : DbContext
    where TEntity : class
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateOrUpdateInput : class
{
    public EfCoreCrudApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public abstract class EfCoreCrudApplicationServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    : ApplicationServiceBase, ICrudApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TContext : DbContext
    where TEntity : class
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    public EfCoreCrudApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
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

    public virtual async ValueTask<OutputResult> CreateAsync(TCreateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return OutputResult.Failed(errors);
        }

        var entity = Mapper.Map<TCreateInput, TEntity>(model);
        Set.Add(entity);
        return await SaveChangesAsync();
    }

    public virtual async ValueTask<OutputResult> DeleteAsync(TKey id)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            return OutputResult.Failed(string.Format(Locale.Message_EntityNotFound, id));
        }
        Set.Remove(entity);
        return await SaveChangesAsync();
    }

    public async ValueTask<OutputResult> UpdateAsync(TKey id, TUpdateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return OutputResult.Failed(errors);
        }

        var entity = await FindAsync(id);
        if (entity is null)
        {
            return OutputResult.Failed(string.Format(Locale.Message_EntityNotFound, id));
        }

        Mapper.Map(model, entity);
        return await SaveChangesAsync();
    }

    public virtual async ValueTask<OutputResult<TGetOutput?>> GetAsync(TKey id)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            return OutputResult<TGetOutput?>.Failed(string.Format(Locale.Message_EntityNotFound, id));
        }
        var output = Mapper.Map<TEntity?, TGetOutput>(entity);
        return OutputResult<TGetOutput?>.Success(output);
    }

    public virtual async Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync(int page, int size, TGetListInput? model = default)
    {
        var query = Query;

        query = CreateQuery(query);

        if (model is IHasQueryable<TEntity> queryable)
        {
            query = queryable.Query(query);
        }

        query = query.Skip((page - 1) * size).Take(page * size);
        try
        {
            var data = await Mapper.ProjectTo<TGetListOutput>(query, model).ToListAsync(CancellationToken);
            var total = await query.CountAsync(CancellationToken);
            return OutputResult<PagedOutputDto<TGetListOutput>>.Success(new(data, total));
        }
        catch (AggregateException ex)
        {
            Logger.LogError(ex, ex.Message);
            return OutputResult<PagedOutputDto<TGetListOutput>>.Failed(ex.Message);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="throwIfError"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected ValueTask<TEntity?> FindAsync(TKey id) => Set.FindAsync(id);

    protected virtual IQueryable<TEntity> CreateQuery(IQueryable<TEntity> source) => source;

    /// <summary>
    /// 保存数据库。
    /// </summary>
    /// <returns></returns>
    protected virtual async Task<OutputResult> SaveChangesAsync()
    {
        try
        {
            var rows = await Context.SaveChangesAsync(CancellationToken);
            if (rows > 0)
            {
                Logger.LogInformation("Save changes successfully");
                return OutputResult.Success();
            }
            Logger.LogWarning("Save changes failed because affected row is 0");
            return OutputResult.Failed("Save changes failed");
        }
        catch (AggregateException ex)
        {
            Logger.LogError(ex, string.Join(";", ex.InnerExceptions.Select(m => m.Message)));
            return OutputResult.Failed("Exceptions occured when saving changes, see log for details");
        }
    }
}
