using AutoMapper;
using Boloni.Data;
using Boloni.Services;
using Microsoft.AspNetCore.Mvc;

namespace Boloni.HttpApi.Controllers;
public abstract class BoloniCrudControllerBase<TAppService, TEntity> : BoloniCrudControllerBase<TAppService, TEntity, TEntity, TEntity, TEntity>
   where TAppService : IApplicationService<TEntity>
    where TEntity : EntityBase
{

}

public abstract class BoloniCrudControllerBase<TAppService, TEntity, TGetDto, TGetListDto, TCreateOrUpdateDto> : BoloniCrudControllerBase<TAppService, TEntity, TGetDto, TGetListDto, TCreateOrUpdateDto, TCreateOrUpdateDto>
   where TAppService : IApplicationService<TEntity>
    where TEntity : EntityBase
     where TGetDto : class
    where TGetListDto : class
    where TCreateOrUpdateDto : class
{

}


public abstract class BoloniCrudControllerBase<TAppService, TEntity, TGetDto, TGetListDto, TCreateDto, TUpdateDto>
    : BoloniControllerBase
   where TAppService : IApplicationService<TEntity>
    where TEntity : EntityBase
     where TGetDto : class
    where TGetListDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    protected IServiceProvider ServiceProvider => base.Request.HttpContext.RequestServices;

    protected TAppService AppService => ServiceProvider.GetRequiredService<TAppService>();

    protected ILogger Logger => ServiceProvider.GetRequiredService<ILogger<TAppService>>();

    protected IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TCreateDto model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var entity = Mapper.Map<TCreateDto, TEntity>(model);

        var createdEntity = await AppService.CreateAsync(entity);

        return CreatedAtAction(nameof(GetAsync), new { id = createdEntity.Id }, Mapper.Map<TEntity, TGetDto>(createdEntity));
    }

    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TUpdateDto model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }


        var updatedEntity = await AppService.UpdateAsync(id, entity =>
        {

            entity = Mapper.Map(model, entity);
        });

        return Ok(Mapper.Map<TEntity, TGetDto>(updatedEntity));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await AppService.DeleteAsync(id);
        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var entity = await AppService.FindAsync(id);

        if (entity is null)
        {
            return NotFound(id);
        }

        var model = Mapper.Map<TEntity, TGetDto>(entity);
        return Ok(model);
    }
}
