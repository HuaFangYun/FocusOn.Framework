using FocusOn.Framework.Business.Contract.DTO;
using FocusOn.Framework.Business.Contract.Identity;
using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Business.Store.Identity;
using FocusOn.Framework.Endpoint.HttpApi.Controllers;
using FocusOn.Framework.Endpoint.HttpApi.Localizations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusOn.Framework.Endpoint.HttpApi.Identity.Controllers;

/// <summary>
/// 基于 <see cref="IdentityRole{TKey}"/> 的角色 CRUD 的 HTTP API 的控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public class IdentityRoleCrudApiController<TContext, TKey> : IdentityRoleCrudApiController<TContext, IdentityRole<TKey>, TKey, IdentityRoleDetailOutput, IdentityRoleListOutput, IdentityRoleListSearchInput, IdentityRoleCreateInput, IdentityRoleUpdateInput>, IIdentityRoleCrudBusinessService<TKey, IdentityRoleDetailOutput, IdentityRoleListOutput, IdentityRoleListSearchInput, IdentityRoleCreateInput, IdentityRoleUpdateInput>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudApiController{TContext, TKey}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudApiController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示角色 CRUD 的 HTTP API 的控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TRole">角色类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public class IdentityRoleCrudApiController<TContext, TRole, TKey> : IdentityRoleCrudApiController<TContext, TRole, TKey, TRole, TRole, TRole, TRole>
    where TContext : DbContext
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudApiController{TContext, TRole, TKey}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudApiController(IServiceProvider serviceProvider) : base(serviceProvider)
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
public class IdentityRoleCrudApiController<TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput> : IdentityRoleCrudApiController<TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput, TCreateOrUpdateInput>, IIdentityRoleCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput>
    where TContext : DbContext
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
    where TDetailOutput : class
    where TListSearchInput : class
    where TListOutput : class
    where TCreateOrUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudApiController{TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateOrUpdateInput}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudApiController(IServiceProvider serviceProvider) : base(serviceProvider)
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
[Route("api/roles")]
public class IdentityRoleCrudApiController<TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput> : CrudApiControllerBase<TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>, IIdentityRoleCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    where TContext : DbContext
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
    where TDetailOutput : class
    where TListSearchInput : class
    where TListOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="IdentityRoleCrudApiController{TContext, TRole, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    public IdentityRoleCrudApiController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 获取指定角色名的角色。
    /// </summary>
    /// <param name="name">角色名称。</param>
    [HttpGet("name/{name}")]
    public virtual async Task<OutputResult<TDetailOutput>> GetByNameAsync(string name)
    {
        var role = await Query.SingleOrDefaultAsync(m => m.Name.Equals(name), CancellationToken);
        if (role is null)
        {
            return OutputResult<TDetailOutput>.Failed(Locale.Message_Role_NameNotFound.StringFormat(name));
        }
        var model = Mapper.Map<TDetailOutput>(role);
        return OutputResult<TDetailOutput>.Success(model);
    }

    /// <summary>
    /// 重写角色创建。
    /// </summary>
    /// <param name="model">要创建的模型。</param>
    /// <returns></returns>
    public override async ValueTask<OutputResult<TDetailOutput>> CreateAsync([FromBody] TCreateInput model)
    {
        var valid = Validator.TryValidate(model, out var errors);
        if (!valid)
        {
            return OutputResult<TDetailOutput>.Failed(errors);
        }

        if (model.GetType() == typeof(TRole))
        {
            var entity = model as TRole;
            Set.Add(entity);
            await SaveChangesAsync();
            return OutputResult<TDetailOutput>.Success(MapToDetail(entity));
        }
        if (model is IdentityRoleCreateInput createInput)
        {
            var result = await GetByNameAsync(createInput.Name);
            if (result.Succeed)
            {
                return OutputResult<TDetailOutput>.Failed(Locale.Message_Role_NameDuplicate.StringFormat(createInput.Name));
            }

            var entity = MapToEntity(model);
            Set.Add(entity);
            await SaveChangesAsync();

            return OutputResult<TDetailOutput>.Success(MapToDetail(entity));
        }


        return OutputResult<TDetailOutput>.Failed($"{nameof(model)} 不是派生自 {nameof(IdentityRoleCreateInput)} 类，请重写并自己实现业务逻辑");
    }
}
