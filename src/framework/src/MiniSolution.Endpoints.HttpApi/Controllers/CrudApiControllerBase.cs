
using Microsoft.AspNetCore.Mvc;

using MiniSolution.Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using MiniSolution.Business.Contracts.DTO;

namespace MiniSolution.Endpoints.HttpApi.Controllers;

public abstract class CrudApiControllerBase<TAppService, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateOrUpdateInputDto>
    : CrudApiControllerBase<TAppService, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateOrUpdateInputDto, TCreateOrUpdateInputDto>
   where TAppService : ICrudApplicationService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateOrUpdateInputDto>

     where TGetOutputDto : class
    where TGetListInputDto : class
    where TGetListOutputDto : class
    where TCreateOrUpdateInputDto : class
{

}


public abstract class CrudApiControllerBase<TAppService, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    : ApiControllerBase, ICrudApplicationService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
   where TAppService : ICrudApplicationService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
     where TGetOutputDto : class
    where TGetListInputDto : class
    where TGetListOutputDto : class
    where TCreateInputDto : class
    where TUpdateInputDto : class
{
    protected TAppService AppService => ServiceProvider.GetRequiredService<TAppService>();

    [HttpPost]
    public virtual ValueTask<OutputResult> CreateAsync(TCreateInputDto model)
    => AppService.CreateAsync(model);

    [HttpDelete("{id}")]
    public virtual ValueTask<OutputResult> DeleteAsync(TKey id)
    => AppService.DeleteAsync(id);
    [HttpGet("{id}")]
    public virtual ValueTask<OutputResult<TGetOutputDto?>> GetAsync(TKey id)
    => AppService.GetAsync(id);
    [HttpGet]
    public virtual Task<OutputResult<PagedOutputDto<TGetListOutputDto>>> GetListAsync(int page = 1, int size = 10, [FromQuery] TGetListInputDto? model = null)
    => AppService.GetListAsync(page, size, model);
    [HttpPut("{id}")]
    public virtual ValueTask<OutputResult> UpdateAsync(TKey id, TUpdateInputDto model)
    => AppService.UpdateAsync(id, model);
}
