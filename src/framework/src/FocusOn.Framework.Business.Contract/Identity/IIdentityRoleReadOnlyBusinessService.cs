﻿using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.Business.Contract.Identity;

/// <summary>
/// 提供具备查询角色的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">输入、输出的结果类型。</typeparam>
public interface IIdentityRoleReadOnlyBusinessService<TKey, TModel> : IIdentityRoleReadOnlyBusinessService<TKey, TModel, TModel, TModel>, IReadOnlyBusinessService<TKey, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{

}

/// <summary>
/// 提供具备查询角色的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">详情输出类型。</typeparam>
/// <typeparam name="TListOutput">列表输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询输入类型。</typeparam>
public interface IIdentityRoleReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput> : IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput>
    where TKey : IEquatable<TKey>
    where TDetailOutput : notnull
    where TListOutput : notnull
    where TListSearchInput : class
{
    /// <summary>
    /// 以异步的方式使用角色名称获取角色的详情。
    /// </summary>
    /// <param name="name">角色名称。</param>
    [Get("by-name/{name}")]
    Task<Return<TDetailOutput>> GetByNameAsync(string name);
}
