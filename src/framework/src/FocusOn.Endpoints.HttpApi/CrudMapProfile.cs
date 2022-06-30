using AutoMapper;

namespace FocusOn.Endpoints.HttpApi;

/// <summary>
/// 定义支持 CRUD 映射的 <see cref="Profile"/> 派生基类。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TGetOutputDto">单个获取的输出 DTO 类型。</typeparam>
/// <typeparam name="TGetListOutputDto">列表获取的输出 DTO 类型。</typeparam>
/// <typeparam name="TCreateInputDto">创建输入 DTO 类型。</typeparam>
/// <typeparam name="TUpdateInputDto">更新输入 DTO 类型。</typeparam>
public abstract class CrudMapProfile<TEntity, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto> : ReadOnlyProfile<TEntity, TGetOutputDto, TGetListOutputDto>
    where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="CrudMapProfile{TEntity, TGetOutputDto, TGetListOutputDto, TCreateInputDto, TUpdateInputDto}"/> 类的新实例。
    /// </summary>
    protected CrudMapProfile() : base()
    {
        CreateMap<TCreateInputDto, TEntity>();
        CreateMap<TUpdateInputDto, TEntity>();
    }
}

/// <summary>
/// 定义支持查询映射的 <see cref="Profile"/> 派生基类。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TGetOutputDto">单个获取的输出 DTO 类型。</typeparam>
/// <typeparam name="TGetListOutputDto">列表获取的输出 DTO 类型。</typeparam>
public abstract class ReadOnlyProfile<TEntity, TGetOutputDto, TGetListOutputDto> : Profile
    where TEntity : class
{
    protected ReadOnlyProfile()
    {
        CreateMap<TEntity, TGetListOutputDto>();
        CreateMap<TEntity, TGetOutputDto>();
    }
}