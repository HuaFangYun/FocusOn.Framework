using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.DTO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FocusOn.Framework.Endpoint.HttpApi.Controllers;

/// <summary>
/// 表示具备 CRUD 功能的 HTTP API 控制器基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public abstract class CrudApiControllerBase<TContext, TEntity, TKey>
    : CrudApiControllerBase<TContext, TEntity, TKey, TEntity, TEntity, TEntity, TEntity>
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
{

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
public abstract class CrudApiControllerBase<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput>
    : CrudApiControllerBase<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
    where TDetailOutput : class
    where TListSearchInput : class
    where TListOutput : class
    where TCreateOrUpdateInput : class
{

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
public abstract class CrudApiControllerBase<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    : ReadOnlyApiControllerBase<TContext, TEntity, TKey, TDetailOutput, TListOutput, TListSearchInput>, ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    where TContext : DbContext
    where TEntity : class
    where TKey : IEquatable<TKey>
    where TDetailOutput : class
    where TListSearchInput : class
    where TListOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    /// <summary>
    /// 创建指定数据。
    /// </summary>
    /// <param name="model">要创建的输入。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    [HttpPost]
    public virtual async ValueTask<OutputResult> CreateAsync([FromBody] TCreateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return OutputResult.Failed(errors);
        }

        var entity = MapToEntity(model);
        Set.Add(entity);
        return await SaveChangesAsync();
    }

    /// <summary>
    /// 删除指定 id 的数据。
    /// </summary>
    /// <param name="id">要删除的 Id。</param>
    [HttpDelete("{id}")]
    public virtual async ValueTask<OutputResult> DeleteAsync(TKey id)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            return OutputResult.Failed(GetEntityNotFoundMessage(id));
        }
        Set.Remove(entity);
        return await SaveChangesAsync();
    }

    /// <summary>
    /// 更新指定 id 的数据。
    /// </summary>
    /// <param name="id">要更新的 Id。</param>
    /// <param name="model">要更新的字段。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    [HttpPut("{id}")]
    public virtual async ValueTask<OutputResult> UpdateAsync(TKey id, [FromBody] TUpdateInput model)
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
            return OutputResult.Failed(GetEntityNotFoundMessage(id));
        }

        MapToEntity(model, entity);
        return await SaveChangesAsync();
    }

    /// <summary>
    /// 保存数据库并返回 <see cref="OutputResult"/> 的结果。
    /// </summary>
    protected virtual async Task<OutputResult> SaveChangesAsync()
    {
        try
        {
            var rows = await Context.SaveChangesAsync(CancellationToken);
            if (rows > 0)
            {
                Logger.LogInformation("数据保存成功");
                return OutputResult.Success();
            }
            Logger.LogWarning("因为影响行数是0，保存失败");
            return OutputResult.Failed("数据保存失败，请查看日志");
        }
        catch (AggregateException ex)
        {
            Logger.LogError(ex, string.Join(";", ex.InnerExceptions.Select(m => m.Message)));
            return OutputResult.Failed("保存发生异常，请查看日志以获得详情");
        }
    }

    protected virtual TEntity? MapToEntity(TCreateInput model)
    {
        if (typeof(TCreateInput) == typeof(TEntity))
        {
            return model as TEntity;
        }
        return Mapper.Map<TCreateInput, TEntity>(model);
    }
    protected virtual TEntity? MapToEntity(TUpdateInput model,TEntity entity)
    {
        if (typeof(TUpdateInput) == typeof(TEntity))
        {
            return model as TEntity;
        }
        return Mapper.Map(model, entity);
    }
}
