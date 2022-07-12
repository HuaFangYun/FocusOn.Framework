using System.Security.Cryptography;
using System.Text;

using FocusOn.Framework.Business.Contract.DTO;
using FocusOn.Framework.Business.Contract.Identity;
using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Business.Store.Identity;
using FocusOn.Framework.Endpoint.HttpApi.Controllers;
using FocusOn.Framework.Endpoint.HttpApi.Localizations;
using FocusOn.Framework.Modules.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusOn.Framework.Endpoint.HttpApi.Identity.Controllers;

/// <summary>
///  提供对用户进行 CRUD 操作的 HTTP API 控制器。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TUser">用户类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
public class IdentityUserCrudApiController<TContext, TUser, TKey> : IdentityUserCrudApiController<TContext, TUser, TKey, TUser, TUser, TUser>, IIdentityUserCrudBusinessService<TKey, TUser>
    where TContext : DbContext
    where TUser : IdentityUser<TKey>, new()
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserCrudApiController{TContext, TUser, TKey}"/> 类的新实例。
    /// </summary>
    public IdentityUserCrudApiController(IServiceProvider serviceProvider) : base(serviceProvider)
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
public class IdentityUserCrudApiController<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput> : IdentityUserCrudApiController<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TUser, TUser>, IIdentityUserCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TUser, TUser>
    where TContext : DbContext
    where TUser : IdentityUser<TKey>, new()
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : class
    where TDetailOutput : class
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserCrudApiController{TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput}"/> 类的新实例。
    /// </summary>
    public IdentityUserCrudApiController(IServiceProvider serviceProvider) : base(serviceProvider)
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
[Route("api/users")]
public class IdentityUserCrudApiController<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput> : CrudApiControllerBase<TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>, IIdentityUserCrudBusinessService<TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput>
    where TContext : DbContext
    where TUser : IdentityUser<TKey>, new()
    where TKey : IEquatable<TKey>
    where TListSearchInput : class
    where TListOutput : class
    where TDetailOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    /// <summary>
    /// 初始化 <see cref="IdentityUserCrudApiController{TContext, TUser, TKey, TDetailOutput, TListOutput, TListSearchInput, TCreateInput, TUpdateInput}"/> 类的新实例。
    /// </summary>
    public IdentityUserCrudApiController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 获取指定用户名的用户详情。
    /// </summary>
    /// <param name="userName">用户名。</param>
    [HttpGet("username/{userName}")]
    public virtual async Task<OutputResult<TDetailOutput>> GetByUserNameAsync(string userName)
    {
        var user = await Query.SingleOrDefaultAsync(m => m.UserName.Equals(userName), CancellationToken);
        if (user is null)
        {
            return OutputResult<TDetailOutput>.Failed(Locale.Message_User_UserNameNotFound.StringFormat(userName));
        }

        var model = Mapper.Map<TDetailOutput>(user);
        return OutputResult<TDetailOutput>.Success(model);
    }

    /// <summary>
    /// 重写用户创建。
    /// </summary>
    /// <param name="model">用户创建模型。</param>
    [HttpPost]
    public override async ValueTask<OutputResult<TDetailOutput>> CreateAsync([FromBody] TCreateInput model)
    {
        var valid = Validator.TryValidate(model, out var errors);
        if (!valid)
        {
            return OutputResult<TDetailOutput>.Failed(errors);
        }

        if (model.GetType() == typeof(TUser))
        {
            var user = model as TUser;
            Set.Add(user);
            await SaveChangesAsync();
            return OutputResult<TDetailOutput>.Success(MapToDetail(user));
        }

        if (model is IdentityUserCreateInput createInput)
        {
            var result = await GetByUserNameAsync(createInput.UserName);
            if (result.Succeed)//用户名重复
            {
                return OutputResult<TDetailOutput>.Failed(Locale.Message_User_UserNameDuplicate.StringFormat(createInput.UserName));
            }

            var user = new TUser
            {
                UserName = createInput.UserName
            };

            if (model is IdentityUserPasswordCreateInput passwordCreateInput)
            {
                user.PasswordHash = HashPassword(passwordCreateInput.Password);
            }

            Set.Add(user);

            await SaveChangesAsync();
            return OutputResult<TDetailOutput>.Success(MapToDetail(user));
        }

        return OutputResult<TDetailOutput>.Failed($"{nameof(model)} 不是派生自 {nameof(IdentityUserCreateInput)} 类，请重写并自己实现业务逻辑");
    }

    /// <summary>
    /// 对指定的原始密码进行哈希加密。
    /// </summary>
    /// <param name="password">原始密码。</param>
    /// <returns>哈希加密后的密码字符串。</returns>
    /// <exception cref="ArgumentException"><paramref name="password"/> 是空或空白字符串。</exception>
    protected virtual string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));
        }

        var passwordHashBuffer = MD5.Create().ComputeHash(Encoding.Default.GetBytes(password));

        return Convert.ToBase64String(passwordHashBuffer);
    }
}
