using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace FocusOn.Framework.Endpoint.HttpApi.Controllers;

/// <summary>
/// 基于 EF Core 的 HTTP API 的基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public abstract class EfCoreApiControllerBase<TContext, TEntity, TKey> : ApiControllerBase
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="EfCoreApiControllerBase{TContext, TEntity, TKey}"/> 类的新实例。
    /// </summary>
    protected EfCoreApiControllerBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 获取 <typeparamref name="TContext"/> 实例。
    /// </summary>
    protected TContext Context => ServiceProvider.GetRequiredService<TContext>();

    /// <summary>
    /// 获取 <see cref="DbSet{TEntity}"/> 实例。
    /// </summary>
    protected DbSet<TEntity> Set => GetDbSet<TEntity>();

    /// <summary>
    /// 获取 <c>AsNoTrackingWithIdentityResolution</c> 的查询结果。
    /// </summary>
    protected IQueryable<TEntity> Query => Set.AsNoTrackingWithIdentityResolution();

    /// <summary>
    /// 获取指定的数据集。
    /// </summary>
    /// <typeparam name="T">实体类型。</typeparam>
    protected DbSet<T> GetDbSet<T>() where T : class
        => Context.Set<T>();
    /// <summary>
    /// 获取可跟踪的实体。
    /// </summary>
    /// <typeparam name="T">实体类型。</typeparam>
    /// <param name="entity">可跟踪的实体。</param>
    protected EntityEntry<T> GetEntityEntry<T>(T entity) where T : class
        => Context.Entry(entity);
}
