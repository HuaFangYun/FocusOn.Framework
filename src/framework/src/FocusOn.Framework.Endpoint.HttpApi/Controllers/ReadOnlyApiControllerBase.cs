using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.DTO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FocusOn.Framework.Endpoint.HttpApi.Controllers;

/// <summary>
/// 表示提供只读查询的 HTTP API 控制器基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
[Produces("application/json")]
public abstract class ReadOnlyApiControllerBase<TContext, TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput> :ApiControllerBase , IReadOnlyBusinessService<TKey,TGetOutput,TGetListOutput,TGetListInput>
    where TContext: DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
{
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
    /// <param name="id">要获取的 Id。</param>
    [HttpGet("{id}")]
    public virtual async ValueTask<OutputResult<TGetOutput?>> GetAsync(TKey id)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            return OutputResult<TGetOutput?>.Failed(GetEntityNotFoundMessage(id));
        }
        var output = MapToModel(entity);
        return OutputResult<TGetOutput?>.Success(output);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="model">列表检索的输入。</param>
    [HttpGet]
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
    /// 根据主键获取指定的实体。如果用这种方式，会有实体跟踪，若仅查询，请使用 <c>Query</c> 属性。
    /// </summary>
    /// <param name="id">主键 Id。</param>
    protected ValueTask<TEntity?> FindAsync(TKey id) => Set.FindAsync(id);

    /// <summary>
    /// 创建指定 <typeparamref name="TGetListInput"/> 提供的检索。
    /// </summary>
    /// <param name="model">获取的输入参数。</param>
    protected virtual IQueryable<TEntity> CreateQuery(TGetListInput? model) => Query;

    /// <summary>
    /// 获取 实体不存在 的消息字符串。
    /// </summary>
    /// <param name="id">实体的 id</param>
    protected virtual string GetEntityNotFoundMessage(TKey id)
        => "实体 '{0}' 不存在".StringFormat(id);

    protected virtual TGetOutput? MapToModel(TEntity entity)
    {
        if (typeof(TEntity) == typeof(TGetOutput))
        {
            return entity as TGetOutput;
        }

        return Mapper.Map<TEntity, TGetOutput>(entity);
    }
}
