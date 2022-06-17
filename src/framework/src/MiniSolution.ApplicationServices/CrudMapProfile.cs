using AutoMapper;

namespace MiniSolution.ApplicationServices;

public abstract class CrudMapProfile<TEntity, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto> : Profile
    where TEntity : class
{
    protected CrudMapProfile()
    {
        CreateMap<TCreateInputDto, TEntity>();
        CreateMap<TUpdateInputDto, TEntity>();
        CreateMap<TEntity, TGetListOutputDto>();
        CreateMap<TEntity, TGetOutputDto>();
    }
}
