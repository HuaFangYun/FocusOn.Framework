using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.Business.Contract;

/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">增改查的输出、输出模型类型。</typeparam>
public interface ICrudBusinessService<in TKey, TModel> : ICrudBusinessService<TKey, TModel, TModel, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{

}
/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOrListOutput">列表或详情的输出模型类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public interface ICrudBusinessService<in TKey, TDetailOrListOutput, in TListSearchInput, in TCreateOrUpdateInput>
    : ICrudBusinessService<TKey, TDetailOrListOutput, TDetailOrListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TDetailOrListOutput : notnull
    where TCreateOrUpdateInput : notnull
{

}

/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public interface ICrudBusinessService<in TKey, TDetailOutput, TListOutput, in TListSearchInput, in TCreateOrUpdateInput>
    : ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : notnull
    where TDetailOutput : notnull
    where TCreateOrUpdateInput : notnull
{

}

/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">单个详情的输出类型。</typeparam>
/// <typeparam name="TListOutput">列表的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public interface ICrudBusinessService<in TKey, TDetailOutput, TListOutput, in TListSearchInput, in TCreateInput, in TUpdateInput> : IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput>, ICreateBusinessService<TDetailOutput, TCreateInput>, IUpdateBusinessService<TDetailOutput, TKey, TUpdateInput>, IDeleteBusinessService<TDetailOutput, TKey>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : notnull
    where TDetailOutput : notnull
    where TCreateInput : notnull
    where TUpdateInput : notnull
{
}
