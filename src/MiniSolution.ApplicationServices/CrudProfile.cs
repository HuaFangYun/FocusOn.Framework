using AutoMapper;

namespace MiniSolution.ApplicationServices
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
