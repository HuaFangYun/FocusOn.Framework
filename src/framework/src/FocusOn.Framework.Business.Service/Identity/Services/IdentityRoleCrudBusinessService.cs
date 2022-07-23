using FocusOn.Framework.Business.Contract.Identity;
using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Business.Services.Localizations;
using FocusOn.Framework.Business.Store.Identity;

using Microsoft.EntityFrameworkCore;

namespace FocusOn.Framework.Business.Services.Identity.Services;

/// <summary>
/// 基于 <see cref="IdentityRole{TKey}"/> 的角色 CRUD 的 HTTP API 的控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public class IdentityRoleCrudBusinessService<TContext, TKey> : IdentityRoleCrudBusinessService<TContext, IdentityRole<TKey>, TKey, IdentityRoleDetailOutput, IdentityRoleListOutput, IdentityRoleListSearchInput, IdentityRoleCreateInput, IdentityRoleUpdateInput>, IIdentityRoleCrudBusinessService<TKey, IdentityRoleDetailOutput, IdentityRoleListOutput, IdentityRoleListSearchInput, IdentityRoleCreateInput, IdentityRoleUpdateInput>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudBusinessService{TContext, TKey}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示角色 CRUD 的 HTTP API 的控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TRole">角色类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public class IdentityRoleCrudBusinessService<TContext, TRole, TKey> : IdentityRoleCrudBusinessService<TContext, TRole, TKey, TRole, TRole, TRole, TRole>
    where TContext : DbContext
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudBusinessService{TContext, TRole, TKey}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示角色 CRUD 的 HTTP API 的控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TRole">角色类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public class IdentityRoleCrudBusinessService<TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput> : IdentityRoleCrudBusinessService<TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, IIdentityRoleCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput>
    where TContext : DbContext
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
    where TDetailOutput : notnull
    where TListSearchInput : class
    where TListOutput : notnull
    where TCreateOrUpdateInput : notnull
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudBusinessService{TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示角色 CRUD 的 HTTP API 的控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TRole">角色类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetailOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TListSearchInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public class IdentityRoleCrudBusinessService<TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput> : CrudBusinessService<TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>, IIdentityRoleCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    where TContext : DbContext
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
    where TDetailOutput : notnull
    where TListSearchInput : class
    where TListOutput : notnull
    where TCreateInput : notnull
    where TUpdateInput : notnull
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudBusinessService{TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudBusinessService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 获取指定角色名的角色。
    /// </summary>
    /// <param name="name">角色名称。</param>
    public virtual async Task<Return<TDetailOutput>> GetByNameAsync(string name)
    {
        var role = await Query.SingleOrDefaultAsync(m => m.Name.Equals(name), CancellationToken);
        if (role is null)
        {
            return Return<TDetailOutput>.Failed(Locale.Message_Role_NameNotFound.StringFormat(name));
        }
        var model = Mapper.Map<TDetailOutput>(role);
        return Return<TDetailOutput>.Success(model);
    }

    /// <summary>
    /// 重写角色创建。
    /// </summary>
    /// <param name="model">要创建的模型。</param>
    /// <returns></returns>
    public override async ValueTask<Return<TDetailOutput>> CreateAsync(TCreateInput model)
    {
        var valid = Validator.TryValidate(model, out var errors);
        if (!valid)
        {
            return Return<TDetailOutput>.Failed(errors);
        }

        if (model.GetType() == typeof(TRole))
        {
            var entity = model as TRole;
            Set.Add(entity);
            await SaveChangesAsync();
            return Return<TDetailOutput>.Success(MapToDetail(entity));
        }
        if (model is IdentityRoleCreateInput createInput)
        {
            var result = await GetByNameAsync(createInput.Name);
            if (result.Succeed)
            {
                return Return<TDetailOutput>.Failed(Locale.Message_Role_NameDuplicate.StringFormat(createInput.Name));
            }

            var entity = MapToEntity(model);
            Set.Add(entity);
            await SaveChangesAsync();

            return Return<TDetailOutput>.Success(MapToDetail(entity));
        }


        return Return<TDetailOutput>.Failed($"{nameof(model)} 不是派生自 {nameof(IdentityRoleCreateInput)} 类，请重写并自己实现业务逻辑");
    }
}
