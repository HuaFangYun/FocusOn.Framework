using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.Business.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FocusOn.Framework.Business.Service;

/// <summary>
/// 基于 EF Core 的 HTTP API 的基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public abstract class EfCoreBusinessServiceBase<TContext, TEntity, TKey> : BusinessServiceBase, IBusinessSerivce
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="EfCoreBusinessServiceBase{TContext, TEntity, TKey}"/> 类的新实例。
    /// </summary>
    protected EfCoreBusinessServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
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
    /// 获取 <c>AsNoTracking</c> 的查询结果。
    /// </summary>
    protected IQueryable<TEntity> Query => Set.AsNoTracking();

    /// <summary>
    /// 获取 <c>AsNoTrackingWithIdentityResolution</c> 的查询结果。
    /// </summary>
    protected IQueryable<TEntity> QueryWithIdentityResolution => Set.AsNoTrackingWithIdentityResolution();

    /// <summary>
    /// 获取指定的数据集。
    /// </summary>
    /// <typeparam name="T">实体类型。</typeparam>
    protected DbSet<T> GetDbSet<T>() where T : class
        => Context.Set<T>();

    /// <summary>
    /// 获取只读查询的数据集。
    /// </summary>
    /// <typeparam name="T">实体类型。</typeparam>
    protected IQueryable<T> GetDbQuery<T>() where T : class
        => GetDbSet<T>().AsNoTrackingWithIdentityResolution();

    /// <summary>
    /// 获取可跟踪的实体。
    /// </summary>
    /// <typeparam name="T">实体类型。</typeparam>
    /// <param name="entity">可跟踪的实体。</param>
    protected EntityEntry<T> GetEntityEntry<T>(T entity) where T : class
        => Context.Entry(entity);
}
