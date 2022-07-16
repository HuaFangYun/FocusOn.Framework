using AutoMapper;

namespace FocusOn.Framework.Endpoint.HttpApi;

/// <summary>
/// 定义支持 CRUD 映射的 <see cref="Profile"/> 派生基类。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TModel">输入输出类型。</typeparam>
public abstract class CrudMapProfile<TEntity, TModel> : CrudMapProfile<TEntity, TModel, TModel>
    where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="CrudMapProfile{TEntity, TModel}"/> 类的新实例。
    /// </summary>
    protected CrudMapProfile() : base()
    {

    }
}

/// <summary>
/// 定义支持 CRUD 映射的 <see cref="Profile"/> 派生基类。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TDetailOrListOutput">列表或详情的输出模型类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public abstract class CrudMapProfile<TEntity, TDetailOrListOutput, TCreateOrUpdateInput> : CrudMapProfile<TEntity, TDetailOrListOutput, TDetailOrListOutput, TCreateOrUpdateInput>
    where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="CrudMapProfile{TEntity, TDetailOrListOutput, TCreateOrUpdateInput}"/> 类的新实例。
    /// </summary>
    protected CrudMapProfile() : base()
    {

    }
}

/// <summary>
/// 定义支持 CRUD 映射的 <see cref="Profile"/> 派生基类。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TDetailOutput">详情的输出类型。</typeparam>
/// <typeparam name="TListOutput">列表的输出类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public abstract class CrudMapProfile<TEntity, TDetailOutput, TListOutput, TCreateOrUpdateInput> : CrudMapProfile<TEntity, TDetailOutput, TListOutput, TCreateOrUpdateInput, TCreateOrUpdateInput>
    where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="CrudMapProfile{TEntity, TDetailOutput, TListOutput, TCreateOrUpdateInput}"/> 类的新实例。
    /// </summary>
    protected CrudMapProfile():base()
    {
    }
}

/// <summary>
/// 定义支持 CRUD 映射的 <see cref="Profile"/> 派生基类。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TDetailOutput">详情的输出类型。</typeparam>
/// <typeparam name="TListOutput">列表的输出类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public abstract class CrudMapProfile<TEntity, TDetailOutput, TListOutput, TCreateInput, TUpdateInput> : ReadOnlyProfile<TEntity, TDetailOutput, TListOutput>
where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="CrudMapProfile{TEntity, TDetailOutput, TListOutput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    protected CrudMapProfile() : base()
    {
        CreateMap<TCreateInput, TEntity>();
        CreateMap<TUpdateInput, TEntity>();
    }
}


/// <summary>
/// 定义支持查询映射的 <see cref="Profile"/> 派生基类。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TDetailOrListOutput">列表或详情的输出模型类型。</typeparam>
public abstract class ReadOnlyProfile<TEntity, TDetailOrListOutput> : ReadOnlyProfile<TEntity, TDetailOrListOutput, TDetailOrListOutput> where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="ReadOnlyProfile{TEntity, TDetailOrListOutput}"/>
    /// </summary>
    protected ReadOnlyProfile() : base()
    {
    }
}

/// <summary>
/// 定义支持查询映射的 <see cref="Profile"/> 派生基类。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TDetailOutput">详情的输出类型。</typeparam>
/// <typeparam name="TListOutput">列表的输出类型。</typeparam>
public abstract class ReadOnlyProfile<TEntity, TDetailOutput, TListOutput> : Profile
    where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="ReadOnlyProfile{TEntity, TDetailOutput, TListOutput}"/> 类的新实例。
    /// </summary>
    protected ReadOnlyProfile()
    {
        CreateMap<TEntity, TListOutput>();
        CreateMap<TEntity, TDetailOutput>();
    }
}