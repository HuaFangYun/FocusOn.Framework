using AutoMapper;

namespace MiniSolution.Business.Services;

/// <summary>
/// 定义支持 CRUD 映射的 <see cref="Profile"/> 派生基类。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TGetOutputDto">单个获取的输出 DTO 类型。</typeparam>
/// <typeparam name="TGetListOutputDto">列表获取的输出 DTO 类型。</typeparam>
/// <typeparam name="TCreateInputDto">创建输入 DTO 类型。</typeparam>
/// <typeparam name="TUpdateInputDto">更新输入 DTO 类型。</typeparam>
public abstract class CrudMapProfile<TEntity, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto> : Profile
    where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="CrudMapProfile{TEntity, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto}"/> 类的新实例。
    /// </summary>
    protected CrudMapProfile()
    {
        CreateMap<TCreateInputDto, TEntity>();
        CreateMap<TUpdateInputDto, TEntity>();
        CreateMap<TEntity, TGetListOutputDto>();
        CreateMap<TEntity, TGetOutputDto>();
    }
}
