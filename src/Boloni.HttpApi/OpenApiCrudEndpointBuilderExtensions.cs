using Boloni.Data.Entities;
using Boloni.Services.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace Boloni.HttpApi
{
    public static class OpenApiCrudEndpointBuilderExtensions
    {
        public static IEndpointRouteBuilder MapCrud<TAppService, TEntity, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>(this IEndpointRouteBuilder builder,string prefix,string groupName="Api")
             where TAppService : ICrudApplicationService<TEntity, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    where TEntity : EntityBase<TKey>
     where TGetOutputDto : class
    where TGetListInputDto : class
    where TGetListOutputDto : class
    where TCreateInputDto : class
    where TUpdateInputDto : class
        {

            builder.MapGet($"{prefix}/{{id}}",([FromServices]TAppService service,TKey id)=> service.GetAsync(id))
                .Produces<ApplicationResult<TGetOutputDto>>()
                .WithTags(groupName)
                ;

            builder.MapPost($"{prefix}/{{page}}",([FromServices]TAppService service, [FromBody]TGetListInputDto model,int page,[FromQuery]int size)=> service.GetPagedListAsync(page,size,model))
                .Produces<ApplicationResult<TGetListOutputDto>>()
                .WithTags(groupName);


            builder.MapGet($"{prefix}/{{page:int}}", ([FromServices] TAppService service, int page, [FromQuery] int size) => service.GetPagedListAsync(page, size))
                .Produces<ApplicationResult<TGetListOutputDto>>()
                .WithTags(groupName);

            builder.MapPost($"{prefix}",([FromServices] TAppService service,[FromBody]TCreateInputDto model)=> service.CreateAsync(model))
                .Produces<ApplicationResult>()
                .WithTags(groupName);

            builder.MapPut($"{prefix}/{{id}}", ([FromServices] TAppService service,TKey id, [FromBody] TUpdateInputDto model) => service.UpdateAsync(id,model))
                .Produces<ApplicationResult>()
                .WithTags(groupName);

            builder.MapDelete($"{prefix}/{{id}}", ([FromServices] TAppService service, TKey id)=> service.DeleteAsync(id))
                .Produces<ApplicationResult>()
                .WithTags(groupName);

            return builder;
        }
    }
}
