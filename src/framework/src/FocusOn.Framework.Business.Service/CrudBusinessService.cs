using FocusOn.Framework.Modules;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.Business.Store;
using FocusOn.Framework.Business.Contract;

namespace FocusOn.Framework.Business.Services;

/// <summary>
/// 表示具备 CRUD 功能的 HTTP API 控制器基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public abstract class CrudBusinessService<TContext, TEntity, TKey>
    : CrudBusinessService<TContext, TEntity, TKey, TEntity, TEntity, TEntity, TEntity>, ICrudBusinessService<TKey, TEntity>
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="CrudBusinessService{TContext, TEntity, TKey}"/> 类的新实例。
    /// </summary>
    protected CrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示具备 CRUD 功能的 HTTP API 控制器基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public abstract class CrudBusinessService<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput>
    : CrudBusinessService<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput>
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
    where TDetailOutput : notnull
    where TListSearchInput : class
    where TListOutput : notnull
    where TCreateOrUpdateInput : notnull
{

    /// <summary>
    /// 初始化 <see cref="CrudBusinessService{TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput}"/> 类的新实例。
    /// </summary>
    protected CrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示具备 CRUD 功能的 HTTP API 控制器基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public abstract class CrudBusinessService<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    : ReadOnlyBusinessServiceBase<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput>, ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
    where TDetailOutput : notnull
    where TListSearchInput : class
    where TListOutput : notnull
    where TCreateInput : notnull
    where TUpdateInput : notnull
{
    /// <summary>
    /// 初始化 <see cref="CrudBusinessService{TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    protected CrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 创建指定数据。
    /// </summary>
    /// <param name="model">要创建的输入。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public virtual async Task<Return<TDetailOutput>> CreateAsync(TCreateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return Return<TDetailOutput>.Failed(Logger, errors);
        }

        var entity = MapToEntity(model);
        Set.Add(entity);
        await SaveChangesAsync();

        var detail = MapToDetail(entity);
        return Return<TDetailOutput>.Success(detail);
    }

    /// <summary>
    /// 删除指定 id 的数据。
    /// </summary>
    /// <param name="id">要删除的 Id。</param>
    public virtual async Task<Return<TDetailOutput>> DeleteAsync(TKey id)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            return Return<TDetailOutput>.Failed(Logger, GetEntityNotFoundMessage(id));
        }
        Set.Remove(entity);
        await SaveChangesAsync();

        var detail = MapToDetail(entity);
        return Return<TDetailOutput>.Success(detail);
    }

    /// <summary>
    /// 更新指定 id 的数据。
    /// </summary>
    /// <param name="id">要更新的 Id。</param>
    /// <param name="model">要更新的字段。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public virtual async Task<Return<TDetailOutput>> UpdateAsync(TKey id, TUpdateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return Return<TDetailOutput>.Failed(Logger, errors);
        }

        var entity = await FindAsync(id);
        if (entity is null)
        {
            return Return<TDetailOutput>.Failed(Logger, GetEntityNotFoundMessage(id));
        }

        entity = MapToEntity(model, entity);

        await SaveChangesAsync();

        var detail = MapToDetail(entity);
        return Return<TDetailOutput>.Success(detail);
    }

    /// <summary>
    /// 保存数据库并返回 <see cref="Return"/> 的结果。
    /// </summary>
    protected virtual async Task<Return> SaveChangesAsync()
    {
        try
        {
            var rows = await Context.SaveChangesAsync(true, CancellationToken);
            if (rows > 0)
            {
                Logger?.LogInformation("数据保存成功");
                return Return.Success();
            }
            Logger?.LogWarning("因为影响行数是0，保存失败");
            return Return.Failed("数据保存失败，请查看日志");
        }
        catch (AggregateException ex)
        {
            Logger?.LogError(ex, string.Join(";", ex.InnerExceptions.Select(m => m.Message)));
            return Return.Failed("保存发生异常，请查看日志以获得详情");
        }
    }

    /// <summary>
    /// 将 <typeparamref name="TCreateInput"/> 映射到 <typeparamref name="TEntity"/>
    /// </summary>
    /// <param name="model">要映射的输入模型。</param>
    /// <returns>映射成功的实体。</returns>
    protected virtual TEntity? MapToEntity(TCreateInput model)
    {
        return Mapper.Map<TCreateInput, TEntity>(model);
    }

    /// <summary>
    /// 将 <typeparamref name="TUpdateInput"/> 映射到现有的 <typeparamref name="TEntity"/> 类型。
    /// </summary>
    /// <param name="model">要映射的输入模型。</param>
    /// <param name="entity">现有的实体。</param>
    /// <returns>映射成功的实体。</returns>
    protected virtual TEntity MapToEntity(TUpdateInput model, TEntity entity)
    {
        if (model is IHasId<TKey> idModel && entity is EntityBase<TKey> idEntity)
        {
            idModel.Id = idEntity.Id;
        }

        return Mapper.Map(model, entity);
    }
}
