using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FocusOn.Business.Services.Localizations;
using FocusOn.Business.Contracts.DTO;
using FocusOn.Business.Contracts;

namespace FocusOn.Business.Services;

/// <summary>
/// 表示基于 EF Core 实现增删改查基本功能的基类。这是一个抽象类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public abstract class EfCoreCrudBusinessServiceBase<TContext, TEntity, TKey>
    : EfCoreCrudBusinessServiceBase<TContext, TEntity, TKey, TEntity, TEntity, TEntity>, ICrudBusinessService<TKey, TEntity, TEntity, TEntity, TEntity>
    where TContext : DbContext
    where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="EfCoreCrudBusinessServiceBase{TContext, TEntity, TKey}"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider"><see cref="IServiceProvider"/> 实例。</param>
    protected EfCoreCrudBusinessServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示基于 EF Core 实现增删改查基本功能的基类。这是一个抽象类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
public abstract class EfCoreCrudBusinessServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput>
    : EfCoreCrudBusinessServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TEntity>, ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TEntity>
    where TContext : DbContext
    where TEntity : class
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
{
    /// <summary>
    /// 初始化 <see cref="EfCoreCrudBusinessServiceBase{TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput}"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider"><see cref="IServiceProvider"/> 实例。</param>
    protected EfCoreCrudBusinessServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示基于 EF Core 实现增删改查基本功能的基类。这是一个抽象类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public abstract class EfCoreCrudBusinessServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    : EfCoreCrudBusinessServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    where TContext : DbContext
    where TEntity : class
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateOrUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="EfCoreCrudBusinessServiceBase{TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput}"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider"><see cref="IServiceProvider"/> 实例。</param>
    protected EfCoreCrudBusinessServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示基于 EF Core 实现增删改查基本功能的基类。这是一个抽象类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public abstract class EfCoreCrudBusinessServiceBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    : BusinessService, ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TContext : DbContext
    where TEntity : class
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="EfCoreCrudBusinessServiceBase{TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider"><see cref="IServiceProvider"/> 实例。</param>
    protected EfCoreCrudBusinessServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }


    /// <summary>
    /// 获取 <see cref="IMapper"/> 实例。
    /// </summary>
    protected IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();


    /// <summary>
    /// 获取 <typeparamref name="TContext"/> 实例。
    /// </summary>
    protected TContext Context => ServiceProvider.GetRequiredService<TContext>();

    /// <summary>
    /// 获取 <see cref="DbSet{TEntity}"/> 实例。
    /// </summary>
    protected DbSet<TEntity> Set => Context.Set<TEntity>();

    /// <summary>
    /// 获取 <c>AsNoTracking</c> 的查询结果。
    /// </summary>
    protected IQueryable<TEntity> Query => Set.AsNoTracking();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="model">要创建的输入。</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id">要删除的 Id。</param>
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id">要更新的 Id。</param>
    /// <param name="model">要更新的字段。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public virtual async ValueTask<OutputResult> UpdateAsync(TKey id, TUpdateInput model)
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id">要获取的 Id。</param>
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="model">列表检索的输入。</param>
    public virtual async Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync(TGetListInput? model = default)
    {
        var query = CreateQuery(model);

        if (model is PagedInputDto pagedInputDto)
        {
            query = query.Skip((pagedInputDto.Page - 1) * pagedInputDto.Size).Take(pagedInputDto.Page * pagedInputDto.Size);
        }

        try
        {
            var data = await Mapper.ProjectTo<TGetListOutput>(query).ToListAsync(CancellationToken);
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
    /// 根据主键获取指定的实体。
    /// </summary>
    /// <param name="id">主键 Id。</param>
    protected ValueTask<TEntity?> FindAsync(TKey id) => Set.FindAsync(id);

    /// <summary>
    /// 创建指定 <typeparamref name="TGetListInput"/> 提供的检索。
    /// </summary>
    /// <param name="model">获取的输入参数。</param>
    protected virtual IQueryable<TEntity> CreateQuery(TGetListInput? model) => Query;


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
