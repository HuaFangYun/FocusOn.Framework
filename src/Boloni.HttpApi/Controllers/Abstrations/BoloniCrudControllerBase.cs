
using System.ComponentModel.DataAnnotations;

using Boloni.Data.Entities;
using Boloni.DataTransfers;
using Boloni.HttpApi.Localizations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boloni.HttpApi.Controllers.Abstrations;

public abstract class BoloniCrudControllerBase<TContext, TEntity, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    : BoloniControllerBase<TContext, TEntity, TKey>
     where TContext:DbContext
    where TEntity : EntityBase<TKey>
    where TCreateInputDto:class
    where TGetListInputDto:class
    where TGetListOutputDto:class
    where TGetOutputDto:class
{
    [HttpPost]
    public virtual async Task<IResult> CreateAsync([FromBody]TCreateInputDto model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var entity = Mapper.Map<TCreateInputDto, TEntity>(model);

        Set.Add(entity);
        await SaveChangesAsync();
        return OutputResult.Success().ToResult();
    }

    [HttpPut("{id}")]
    public virtual async Task<IResult> UpdateAsync(TKey id, [FromBody] TUpdateInputDto model)
    {
        var entity = await Set.FindAsync(id);

        if(entity is null)
        {
            return OutputResult.Failed(string.Format(Locale.Message_EntityNotFound, id)).ToResult();
        }

        Mapper.Map(model,entity);

        await SaveChangesAsync();

        return OutputResult.Success().ToResult();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IResult> DeleteAsync(TKey id)
    {
        var entity=await Set.FindAsync(id);

        if (entity is null)
        {
            return OutputResult.Failed(string.Format(Locale.Message_EntityNotFound, id)).ToResult();
        }

        Set.Remove(entity);

        await SaveChangesAsync();

        return OutputResult.Success().ToResult();
    }

    [HttpGet("{id}")]
    public virtual async Task<IResult> GetAsync(TKey id)
    {
        var entity = await Set.FindAsync(id);

        if (entity is null)
        {
            return OutputResult.Failed(string.Format(Locale.Message_EntityNotFound, id)).ToResult();
        }

        var model = Mapper.Map<TEntity, TGetOutputDto>(entity);

        return OutputModel<TGetOutputDto>.Success(model).ToResult();
    }

    [HttpGet]
    public virtual async Task<IResult> GetListAsync([FromQuery] TGetListInputDto? model, [FromQuery]int page=1, [FromQuery]int size=10)
    {
        var query = Query;

        query = CreateQuery(query);

        query = query.Skip((page - 1) * size).Take(page * size);

        var data = await Mapper.ProjectTo<TGetListOutputDto>(query).ToListAsync(CancellationToken);

        var total = await query.CountAsync(CancellationToken);

        return OutputModel<PagedOutputDto<TGetListOutputDto>>.Success(new PagedOutputDto<TGetListOutputDto>(data, total)).ToResult();
    }

    protected virtual IQueryable<TEntity> CreateQuery(IQueryable<TEntity> source)
    {
        return source;
    }
}
