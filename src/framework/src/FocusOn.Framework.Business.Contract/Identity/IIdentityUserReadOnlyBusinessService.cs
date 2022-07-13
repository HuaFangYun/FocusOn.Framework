﻿using FocusOn.Framework.Business.Contract.DTO;

namespace FocusOn.Framework.Business.Contract.Identity;

/// <summary>
/// 提供具备查询的用户业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">输入、输出的结果类型。</typeparam>
public interface IIdentityUserReadOnlyBusinessService<TKey, TModel> : IIdentityUserReadOnlyBusinessService<TKey, TModel, TModel, TModel>, IReadOnlyBusinessService<TKey, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{

}

/// <summary>
/// 提供具备查询的用户业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">详情输出类型。</typeparam>
/// <typeparam name="TListOutput">列表输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表查询输入类型。</typeparam>
public interface IIdentityUserReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput> : IReadOnlyBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput>
    where TKey : IEquatable<TKey>
    where TDetailOutput : class
    where TListOutput : class
    where TListSearchInput : class
{
    /// <summary>
    /// 以异步的方式使用用户名获取用户的详情。
    /// </summary>
    /// <param name="userName">用户名。</param>
    Task<OutputResult<TDetailOutput>> GetByUserNameAsync(string userName);
}