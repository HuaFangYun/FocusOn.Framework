namespace FocusOn.Framework.Business.Contract.Identity;


/// <summary>
/// 提供用户的 CURD 业务功能。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">输入、输出类型。</typeparam>
public interface IIdentityUserCrudBusinessService<TKey, TModel> : IIdentityUserCrudBusinessService<TKey, TModel, TModel, TModel, TModel>, ICrudBusinessService<TKey, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{

}

/// <summary>
/// 提供用户的 CURD 业务功能。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">详情输出类型。</typeparam>
/// <typeparam name="TListOutput">列表输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新输入类型。</typeparam>
public interface IIdentityUserCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput> : IIdentityUserCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput>
    where TKey : IEquatable<TKey>
    where TDetailOutput : class
    where TListOutput : class
    where TListSearchInput : class
    where TCreateOrUpdateInput : class
{

}


/// <summary>
/// 提供用户的 CURD 业务功能。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">详情输出类型。</typeparam>
/// <typeparam name="TListOutput">列表输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新输入类型。</typeparam>
public interface IIdentityUserCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput> : IIdentityUserReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput>, ICrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    where TKey : IEquatable<TKey>
    where TDetailOutput : class
    where TListOutput : class
    where TListSearchInput : class
    where TCreateInput : class
    where TUpdateInput : class
{
}
