using AutoMapper;

using Boloni.Data.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boloni.HttpApi.Controllers.Abstrations;
public abstract class BoloniControllerBase<TContext, TEntity, TKey> : BoloniControllerBase<TContext> where TContext : DbContext
     where TEntity : EntityBase<TKey>
{
    protected DbSet<TEntity> Set => Context.Set<TEntity>();
    protected IQueryable<TEntity> Query => Set.AsNoTracking();
}

public abstract class BoloniControllerBase<TContext> : BoloniControllerBase where TContext : DbContext
{
    protected TContext Context => ServiceProvider.GetRequiredService<TContext>();
    protected virtual Task<int> SaveChangesAsync()
    {
        return Context.SaveChangesAsync(CancellationToken);
    }
}

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class BoloniControllerBase : ControllerBase
{

    protected IServiceProvider ServiceProvider => Request.HttpContext.RequestServices;

    protected ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();
    protected ILogger Logger => LoggerFactory.CreateLogger(GetType().Name);

    protected IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

    protected virtual CancellationToken CancellationToken
    {
        get
        {
            var tokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));
            return tokenSource.Token;
        }
    }
}
