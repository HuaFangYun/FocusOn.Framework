using AutoMapper;

using Boloni.Data.Entities;
using Boloni.DataTransfers;
using Boloni.Services.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace Boloni.HttpApi.Controllers.Abstrations;
public abstract class BoloniCrudControllerBase<TAppService, TEntity, TKey> : BoloniCrudControllerBase<TAppService, TEntity, TKey, TEntity, TEntity, TEntity, TEntity>
   where TAppService : ICrudApplicationService<TEntity, TKey>
    where TEntity : EntityBase<TKey>
{

}

public abstract class BoloniCrudControllerBase<TAppService, TEntity, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateOrUpdateInputDto>
    : BoloniCrudControllerBase<TAppService, TEntity, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateOrUpdateInputDto, TCreateOrUpdateInputDto>
   where TAppService : ICrudApplicationService<TEntity, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateOrUpdateInputDto>
    where TEntity : EntityBase<TKey>
     where TGetOutputDto : class
    where TGetListInputDto : class
    where TGetListOutputDto : class
    where TCreateOrUpdateInputDto : class
{

}


public abstract class BoloniCrudControllerBase<TAppService, TEntity, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    : BoloniControllerBase
   where TAppService : ICrudApplicationService<TEntity, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    where TEntity : EntityBase<TKey>
     where TGetOutputDto : class
    where TGetListInputDto : class
    where TGetListOutputDto : class
    where TCreateInputDto : class
    where TUpdateInputDto : class
{
    protected TAppService AppService => ServiceProvider.GetRequiredService<TAppService>();

    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync([FromBody] TCreateInputDto model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var result = await AppService.CreateAsync(model);
        if (result.Succeed)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> UpdateAsync(TKey id, [FromBody] TUpdateInputDto model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }


        var result = await AppService.UpdateAsync(id, model);

        if (result.Succeed)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(TKey id)
    {
        var result = await AppService.DeleteAsync(id);
        if (result.Succeed)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetAsync(TKey id)
    {
        var result = await AppService.GetAsync(id);

        if (result is null)
        {
            return NotFound(id);
        }
        return Ok(result);
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetListAsync([FromBody] TGetListInputDto model)
    {
        var result = await AppService.GetListAsync(model);
        return Ok(result);
    }

    [HttpGet("{page}")]
    public virtual async Task<IActionResult> GetListAsync([FromBody] TGetListInputDto model, int page, [FromQuery] int size = 10)
    {
        var result = await AppService.GetPagedListAsync(page, size, model);

        return Ok(new
        {
            result.Data,
            result.Total
        });
    }
}
