using AutoMapper;

namespace Boloni.Services.Abstractions
{
    public abstract class CrudProfile<TEntity, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto> : Profile
    {
        protected CrudProfile()
        {
            CreateMap<TCreateInputDto, TEntity>();
            CreateMap<TUpdateInputDto, TEntity>();
            CreateMap<TEntity, TGetListOutputDto>();
            CreateMap<TEntity, TGetOutputDto>();
        }
    }
}
