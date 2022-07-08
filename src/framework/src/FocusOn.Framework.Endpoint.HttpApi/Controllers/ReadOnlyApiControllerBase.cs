using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.DTO;
using FocusOn.Framework.Modules;

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
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
[Produces("application/json")]
public abstract class ReadOnlyApiControllerBase<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput> : EfCoreApiControllerBase<TContext,TEntity,TKey>, IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput>
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : class
    where TDetailOutput : class
{

    /// <summary>
    /// 初始化 <see cref="ReadOnlyApiControllerBase{TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput}"/> 类的新实例。
    /// </summary>
    protected ReadOnlyApiControllerBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id">要获取的 Id。</param>
    [HttpGet("{id}")]
    public virtual async ValueTask<OutputResult<TDetailOutput?>> GetAsync(TKey id)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            return OutputResult<TDetailOutput?>.Failed(GetEntityNotFoundMessage(id));
        }
        var output = MapToDetail(entity);
        return OutputResult<TDetailOutput?>.Success(output);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="model">列表检索的输入。</param>
    [HttpGet]
    public virtual async Task<OutputResult<PagedOutputDto<TListOutput>>> GetListAsync(TListSearchInput? model = default)
    {
        var query = CreateQuery(model);

        if (model is PagedInputDto pagedInputDto)
        {
            query = query.Skip((pagedInputDto.Page - 1) * pagedInputDto.Size).Take(pagedInputDto.Page * pagedInputDto.Size);
        }

        query = ApplyOrderBy(query);

        try
        {
            var data = await Mapper.ProjectTo<TListOutput>(query).ToListAsync(CancellationToken);
            var total = await query.CountAsync(CancellationToken);
            return OutputResult<PagedOutputDto<TListOutput>>.Success(new(data, total));
        }
        catch (AggregateException ex)
        {
            Logger?.LogError(ex, ex.Message);
            return OutputResult<PagedOutputDto<TListOutput>>.Failed(ex.Message);
        }
    }


    /// <summary>
    /// 根据主键获取指定的实体。如果用这种方式，会有实体跟踪，若仅查询，请使用 <c>Query</c> 属性。
    /// </summary>
    /// <param name="id">主键 Id。</param>
    protected ValueTask<TEntity?> FindAsync(TKey id) => Set.FindAsync(id);

    /// <summary>
    /// 创建指定 <typeparamref name="TListSearchInput"/> 提供的检索。
    /// </summary>
    /// <param name="model">获取的输入参数。</param>
    protected virtual IQueryable<TEntity> CreateQuery(TListSearchInput? model) => Query;

    /// <summary>
    /// 获取 实体不存在 的消息字符串。
    /// </summary>
    /// <param name="id">实体的 id</param>
    protected virtual string GetEntityNotFoundMessage(TKey id)
        => "实体 '{0}' 不存在".StringFormat(id);

    /// <summary>
    /// 将 <typeparamref name="TEntity"/> 类型映射到 <typeparamref name="TDetailOutput"/> 类型。
    /// </summary>
    /// <param name="entity">要映射的实体。</param>
    /// <returns>输出类型。</returns>
    protected virtual TDetailOutput? MapToDetail(TEntity entity) => Mapper.Map<TEntity, TDetailOutput>(entity);

    /// <summary>
    /// 应用列表排序算法。
    /// </summary>
    /// <param name="source">要排序的数据源。</param>
    /// <returns>排序后的数据源。</returns>
    /// <exception cref="InvalidOperationException">无法完成排序。</exception>
    protected virtual IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> source)
    {
        if (typeof(TEntity).IsAssignableTo(typeof(IHasCreateTime)))
        {
            return source.OrderByDescending(e => ((IHasCreateTime)e).CreateTime);
        }

        if (typeof(TEntity).IsAssignableTo(typeof(IHasId<TKey>)))
        {
            return source.OrderByDescending(e => ((IHasId<TKey>)e).Id);
        }

        throw new InvalidOperationException($"没有找到列表的排序算法，请重写'{nameof(ApplyOrderBy)}'方法实现列表排序算法");
    }
}
