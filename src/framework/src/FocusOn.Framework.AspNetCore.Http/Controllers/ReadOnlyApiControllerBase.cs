//using FocusOn.Framework.Business.Contract;
//using FocusOn.Framework.Business.Contract.DTO;
//using FocusOn.Framework.Modules;

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace FocusOn.Framework.AspNetCore.Http.Controllers;

///// <summary>
///// 表示提供只读查询的 HTTP API 控制器基类。
///// </summary>
///// <typeparam name="TContext">数据库上下文类型。</typeparam>
///// <typeparam name="TEntity">实体类型。</typeparam>
///// <typeparam name="TKey">主键类型。</typeparam>
//public abstract class ReadOnlyApiControllerBase<TContext, TEntity, TKey> : ReadOnlyApiControllerBase<TContext, TEntity, TKey, TEntity>, IReadOnlyBusinessService<TKey, TEntity>
//        where TContext : DbContext
//    where TEntity : class
//    where TKey : IEquatable<TKey>
//{
//    /// <summary>
//    /// 初始化 <see cref="ReadOnlyApiControllerBase{TContext, TEntity, TKey}"/> 类的新实例。
//    /// </summary>
//    /// <param name="serviceProvider"></param>
//    protected ReadOnlyApiControllerBase(IServiceProvider serviceProvider) : base(serviceProvider)
//    {
//    }
//}

///// <summary>
///// 表示提供只读查询的 HTTP API 控制器基类。
///// </summary>
///// <typeparam name="TContext">数据库上下文类型。</typeparam>
///// <typeparam name="TEntity">实体类型。</typeparam>
///// <typeparam name="TKey">主键类型。</typeparam>
///// <typeparam name="TModel">详情、列表的输出类型模型和列表查询的输入模型类型。</typeparam>
//public abstract class ReadOnlyApiControllerBase<TContext, TEntity, TKey, TModel> : ReadOnlyApiControllerBase<TContext, TEntity, TKey, TModel, TModel>, IReadOnlyBusinessService<TKey, TModel>
//        where TContext : DbContext
//    where TEntity : class
//    where TKey : IEquatable<TKey>
//    where TModel : class
//{
//    /// <summary>
//    /// 初始化 <see cref="ReadOnlyApiControllerBase{TContext, TEntity, TKey, TModel}"/> 类的新实例。
//    /// </summary>
//    /// <param name="serviceProvider"></param>
//    protected ReadOnlyApiControllerBase(IServiceProvider serviceProvider) : base(serviceProvider)
//    {
//    }
//}


///// <summary>
///// 表示提供只读查询的 HTTP API 控制器基类。
///// </summary>
///// <typeparam name="TContext">数据库上下文类型。</typeparam>
///// <typeparam name="TEntity">实体类型。</typeparam>
///// <typeparam name="TKey">主键类型。</typeparam>
///// <typeparam name="TDetailOrListOutput">列表或详情的输出模型类型。</typeparam>
///// <typeparam name="TListSearchInput">列表查询的输入类型。</typeparam>
//public abstract class ReadOnlyApiControllerBase<TContext, TEntity, TKey, TDetailOrListOutput, TListSearchInput> : ReadOnlyApiControllerBase<TContext, TEntity, TKey, TDetailOrListOutput, TDetailOrListOutput, TListSearchInput>
//, IReadOnlyBusinessService<TKey, TDetailOrListOutput, TListSearchInput>
//        where TContext : DbContext
//    where TEntity : class
//    where TKey : IEquatable<TKey>
//    where TListSearchInput : class
//    where TDetailOrListOutput : class
//{
//    /// <summary>
//    /// 初始化 <see cref="ReadOnlyApiControllerBase{TContext, TEntity, TKey, TDetailOrListOutput, TListSearchInput}"/> 类的新实例。
//    /// </summary>
//    /// <param name="serviceProvider"></param>
//    protected ReadOnlyApiControllerBase(IServiceProvider serviceProvider) : base(serviceProvider)
//    {
//    }
//}


///// <summary>
///// 表示提供只读查询的 HTTP API 控制器基类。
///// </summary>
///// <typeparam name="TContext">数据库上下文类型。</typeparam>
///// <typeparam name="TEntity">实体类型。</typeparam>
///// <typeparam name="TKey">主键类型。</typeparam>
///// <typeparam name="TDetailOutput">详情的输出类型。</typeparam>
///// <typeparam name="TListOutput">列表的输出类型。</typeparam>
///// <typeparam name="TListSearchInput">列表查询的输入类型。</typeparam>
//[Produces("application/json")]
//public abstract class ReadOnlyApiControllerBase<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput> : EfCoreApiControllerBase<TContext, TEntity, TKey>, IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput>
//    where TContext : DbContext
//    where TEntity : class
//    where TKey : IEquatable<TKey>
//    where TListSearchInput : class
//    where TListOutput : class
//    where TDetailOutput : class
//{

//    /// <summary>
//    /// 初始化 <see cref="ReadOnlyApiControllerBase{TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput}"/> 类的新实例。
//    /// </summary>
//    protected ReadOnlyApiControllerBase(IServiceProvider serviceProvider) : base(serviceProvider)
//    {
//    }


//    /// <summary>
//    /// <inheritdoc/>
//    /// </summary>
//    /// <param name="id">要获取的 Id。</param>
//    [HttpGet("{id}")]
//    public virtual async ValueTask<Return<TDetailOutput?>> GetAsync(TKey id)
//    {
//        var entity = await FindAsync(id);
//        if (entity is null)
//        {
//            return Return<TDetailOutput?>.Failed(GetEntityNotFoundMessage(id));
//        }
//        var output = MapToDetail(entity);
//        return Return<TDetailOutput?>.Success(output);
//    }

//    /// <summary>
//    /// <inheritdoc/>
//    /// </summary>
//    /// <param name="model">列表检索的输入。</param>
//    [HttpGet]
//    public virtual async Task<Return<PagedOutput<TListOutput>>> GetListAsync(TListSearchInput? model = default)
//    {
//        var query = CreateQuery(model);

//        if (model is PagedInput pagedInputDto)
//        {
//            query = query.Skip((pagedInputDto.Page - 1) * pagedInputDto.Size).Take(pagedInputDto.Page * pagedInputDto.Size);
//        }

//        query = ApplyOrderBy(query);

//        try
//        {
//            var data = await Mapper.ProjectTo<TListOutput>(query).ToListAsync(CancellationToken);
//            var total = await query.CountAsync(CancellationToken);
//            return Return<PagedOutput<TListOutput>>.Success(new(data, total));
//        }
//        catch (AggregateException ex)
//        {
//            return Return<PagedOutput<TListOutput>>.Failed(Logger, ex);
//        }
//    }


//    /// <summary>
//    /// 根据主键获取指定的实体。如果用这种方式，会有实体跟踪，若仅查询，请使用 <c>Query</c> 属性。
//    /// </summary>
//    /// <param name="id">主键 Id。</param>
//    protected ValueTask<TEntity?> FindAsync(TKey id) => Set.FindAsync(id);

//    /// <summary>
//    /// 重写创建数据库的查询条件。该方法适用于每一次的查询。
//    /// </summary>
//    /// <param name="model">条件查询的模型。</param>
//    protected virtual IQueryable<TEntity> CreateQuery(TListSearchInput? model) => Query;

//    /// <summary>
//    /// 获取 实体不存在 的消息字符串。
//    /// </summary>
//    /// <param name="id">实体的 id</param>
//    protected virtual string GetEntityNotFoundMessage(TKey id)
//        => "实体 '{0}' 不存在".StringFormat(id);

//    /// <summary>
//    /// 将 <typeparamref name="TEntity"/> 类型映射到 <typeparamref name="TDetailOutput"/> 类型。
//    /// </summary>
//    /// <param name="entity">要映射的实体。</param>
//    /// <returns>输出类型。</returns>
//    protected virtual TDetailOutput? MapToDetail(TEntity entity) => Mapper.Map<TEntity, TDetailOutput>(entity);

//    /// <summary>
//    /// 应用列表排序算法。
//    /// </summary>
//    /// <param name="source">要排序的数据源。</param>
//    /// <returns>排序后的数据源。</returns>
//    /// <exception cref="InvalidOperationException">无法完成排序。</exception>
//    protected virtual IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> source)
//    {
//        if (typeof(TEntity).IsAssignableTo(typeof(IHasCreateTime)))
//        {
//            return source.OrderByDescending(e => ((IHasCreateTime)e).CreateTime);
//        }

//        if (typeof(TEntity).IsAssignableTo(typeof(IHasId<TKey>)))
//        {
//            return source.OrderByDescending(e => ((IHasId<TKey>)e).Id);
//        }

//        throw new InvalidOperationException($"没有找到列表的排序算法，请重写'{nameof(ApplyOrderBy)}'方法实现列表排序算法");
//    }
//}
