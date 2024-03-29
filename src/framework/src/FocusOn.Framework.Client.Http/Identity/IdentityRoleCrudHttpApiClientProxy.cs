﻿using FocusOn.Framework.Business.Contract.Identity;
using FocusOn.Framework.Business.Contract.Identity.DTO;

namespace FocusOn.Framework.Client.Http.Identity;

/// <summary>
/// 表示角色的 CRUD 的 HTTP API 远程服务代理。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
public class IdentityRoleCrudHttpApiClientProxy<TKey> : IdentityRoleCrudHttpApiClientProxy<TKey, IdentityRoleDetailOutput, IdentityRoleListOutput, IdentityRoleListSearchInput, IdentityRoleCreateInput, IdentityRoleUpdateInput>
        where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudHttpApiClientProxy{TKey}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }
}

/// <summary>
/// 表示角色的 CRUD 的 HTTP API 远程服务代理。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public class IdentityRoleCrudHttpApiClientProxy<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput> : CrudHttpApiClientProxyBase<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>, IIdentityRoleCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
        where TKey : IEquatable<TKey>
    where TDetailOutput : class
    where TListOutput : class
    where TListSearchInput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudHttpApiClientProxy{TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? RootPath => "api/roles";

    /// <summary>
    /// 获取指定角色名称的角色远程服务。
    /// </summary>
    /// <param name="name">角色名称。</param>
    public virtual async Task<Return<TDetailOutput>> GetByNameAsync(string name)
    {
        var uri = GetRequestUri(queryParameters: new { name });
        var response = await Client.GetAsync(uri);
        return await HandleOutputResultAsync<TDetailOutput>(response);
    }
}
