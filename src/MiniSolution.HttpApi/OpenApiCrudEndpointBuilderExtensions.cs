
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using MiniSolution.ApplicationContracts;
using MiniSolution.ApplicationContracts.DTO;

namespace MiniSolution.HttpApi
{
    public static class OpenApiCrudEndpointBuilderExtensions
    {
        public static IEndpointRouteBuilder MapCrud<TAppService, TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>(this IEndpointRouteBuilder builder,string prefix,string groupName="Api")
             where TAppService : ICrudApplicationService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
     where TGetOutputDto : class
    where TGetListInputDto : class
    where TGetListOutputDto : class
    where TCreateInputDto : class
    where TUpdateInputDto : class
        {
            builder.MapGet($"{prefix}/{{id}}",([FromServices]TAppService service,TKey id)=> service.GetAsync(id))
                .Produces<OutputResult<TGetOutputDto>>()
                .WithTags(groupName)
                ;

            builder.MapPost($"{prefix}/{{page}}",([FromServices]TAppService service, [FromForm]TGetListInputDto model,int page,[FromQuery]int size)=> service.GetListAsync(page,size,model))
                .Produces<OutputResult<TGetListOutputDto>>()
                .WithTags(groupName);

            builder.MapPost($"{prefix}",([FromServices] TAppService service,[FromBody]TCreateInputDto model)=> service.CreateAsync(model))
                .Produces<OutputResult>()
                .WithTags(groupName);

            builder.MapPut($"{prefix}/{{id}}", ([FromServices] TAppService service,TKey id, [FromBody] TUpdateInputDto model) => service.UpdateAsync(id,model))
                .Produces<OutputResult>()
                .WithTags(groupName);

            builder.MapDelete($"{prefix}/{{id}}", ([FromServices] TAppService service, TKey id)=> service.DeleteAsync(id))
                .Produces<OutputResult>()
                .WithTags(groupName);

            return builder;
        }
    }
}
