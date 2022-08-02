using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.Business.Contract.Http;
using Microsoft.Extensions.DependencyInjection;
using FocusOn.Framework.Business.DbStore.Identity;
using FocusOn.Framework.Business.Contract.Identity;
using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Business.Service.Localizations;

namespace FocusOn.Framework.Business.Service.Identity.Services;

/// <summary>
///  提供对用户进行 CRUD 操作的 HTTP API 控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TUser">用户类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public class IdentityUserCrudBusinessService<TContext, TUser, TKey> : IdentityUserCrudBusinessService<TContext, TUser, TKey, TUser, TUser, TUser>, IIdentityUserCrudBusinessService<TKey, TUser>
    where TContext : DbContext
    where TUser : IdentityUser<TKey>, new()
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserCrudBusinessService{TContext, TUser, TKey}"/> 类的新实例。
    /// </summary>
    public IdentityUserCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
///  提供对用户进行 CRUD 操作的 HTTP API 控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TUser">用户类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">详情输出类型。</typeparam>
/// <typeparam name="TListOutput">列表输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表搜索输入类型。</typeparam>
public class IdentityUserCrudBusinessService<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput> : IdentityUserCrudBusinessService<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TUser, TUser>, IIdentityUserCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TUser, TUser>
    where TContext : DbContext
    where TUser : IdentityUser<TKey>, new()
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : notnull
    where TDetailOutput : notnull
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserCrudBusinessService{TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput}"/> 类的新实例。
    /// </summary>
    public IdentityUserCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
/// <summary>
///  提供对用户进行 CRUD 操作的 HTTP API 控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TUser">用户类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">详情输出类型。</typeparam>
/// <typeparam name="TListOutput">列表输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表搜索输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新输入类型。</typeparam>
public class IdentityUserCrudBusinessService<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput> : IdentityUserCrudBusinessService<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, IIdentityUserCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput>
        where TContext : DbContext
    where TUser : IdentityUser<TKey>, new()
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : notnull
    where TDetailOutput : notnull
    where TCreateOrUpdateInput : notnull
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudBusinessService{TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput}"/> 类的新实例。
    /// </summary>
    public IdentityUserCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
/// <summary>
///  提供对用户进行 CRUD 操作的 HTTP API 控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TUser">用户类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">详情输出类型。</typeparam>
/// <typeparam name="TListOutput">列表输出类型。</typeparam>
/// <typeparam name="TListSearchInput">列表搜索输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新输入类型。</typeparam>
public class IdentityUserCrudBusinessService<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput> : CrudBusinessService<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>, IIdentityUserCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    where TContext : DbContext
    where TUser : IdentityUser<TKey>, new()
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : notnull
    where TDetailOutput : notnull
    where TCreateInput : notnull
    where TUpdateInput : notnull
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserCrudBusinessService{TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    public IdentityUserCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 获取注册过的 <see cref="IHashPasswordService"/> 实例。
    /// </summary>
    protected IHashPasswordService HashPasswordService => ServiceProvider.GetRequiredService<IHashPasswordService>();

    /// <summary>
    /// 获取指定用户名的用户详情。
    /// </summary>
    /// <param name="userName">用户名。</param>
    public virtual async Task<Return<TDetailOutput>> GetByUserNameAsync(string userName)
    {
        var user = await Query.SingleOrDefaultAsync(m => m.UserName.Equals(userName), CancellationToken);
        if (user is null)
        {
            return Return<TDetailOutput>.Failed(Locale.Message_User_UserNameNotFound.StringFormat(userName));
        }

        var model = Mapper.Map<TDetailOutput>(user);
        return Return<TDetailOutput>.Success(model);
    }

    /// <summary>
    /// 重写用户创建。
    /// </summary>
    /// <param name="model">用户创建模型。</param>
    public override async Task<Return<TDetailOutput>> CreateAsync(TCreateInput model)
    {
        var valid = Validator.TryValidate(model, out var errors);
        if (!valid)
        {
            return Return<TDetailOutput>.Failed(errors);
        }

        if (model.GetType() == typeof(TUser))
        {
            var user = model as TUser;
            Set.Add(user);
            await SaveChangesAsync();
            return Return<TDetailOutput>.Success(MapToDetail(user));
        }

        if (model is IdentityUserCreateInput createInput)
        {
            var result = await GetByUserNameAsync(createInput.UserName);
            if (result.Succeed)//用户名重复
            {
                return Return<TDetailOutput>.Failed(Locale.Message_User_UserNameDuplicate.StringFormat(createInput.UserName));
            }

            var user = MapToEntity(model);

            if (model is IdentityUserPasswordCreateInput passwordCreateInput)
            {
                user.PasswordHash = HashPasswordService.Hash(passwordCreateInput.Password);
            }

            Set.Add(user);

            await SaveChangesAsync();
            var detail = MapToDetail(user);
            return Return<TDetailOutput>.Success(detail);
        }

        return Return<TDetailOutput>.Failed($"{nameof(model)} 不是派生自 {nameof(IdentityUserCreateInput)} 类，请重写并自己实现业务逻辑");
    }

}
