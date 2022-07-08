using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace FocusOn.Framework.Endpoint.HttpApi.Controllers;

public abstract class EfCoreApiControllerBase<TContext, TEntity, TKey> : ApiControllerBase
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
{
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
