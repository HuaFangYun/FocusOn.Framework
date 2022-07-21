using FocusOn.Framework.Business.Contract.Identity;

namespace FocusOn.Framework.Endpoint.HttpProxy.Identity;

/// <summary>
/// 表示用户 CRUD 的 HTTP API 客户端代理。
/// </summary>
public class IdentityUserCrudHttpApiClientProxy<TKey, TModel> : IdentityUserCrudHttpApiClientProxy<TKey, TModel, TModel, TModel, TModel, TModel>,
    IIdentityRoleCrudBusinessService<TKey, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserCrudHttpApiClientProxy{TKey, TModel}"/> 类的新实例。
    /// </summary>
    public IdentityUserCrudHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }
}

/// <summary>
/// 表示用户 CRUD 的 HTTP API 客户端代理。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public class IdentityUserCrudHttpApiClientProxy<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput> : CrudHttpApiClientProxyBase<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>, IIdentityUserCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : class
    where TDetailOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserCrudHttpApiClientProxy{TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    public IdentityUserCrudHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? RootPath => "api/users";

    /// <summary>
    /// 获取指定用户名的用户信息的远程服务。
    /// </summary>
    /// <param name="userName">用户名。</param>
    /// <returns></returns>
    public virtual async Task<Return<TDetailOutput>> GetByUserNameAsync(string userName)
    {
        var uri = GetRequestUri($"username/{userName}");
        var response = await Client.GetAsync(uri);
        return await HandleOutputResultAsync<TDetailOutput>(response);
    }
}
