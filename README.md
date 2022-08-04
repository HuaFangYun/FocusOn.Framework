# FocusOn.Framework
基于契约的自动化前后端分离的全栈应用框架。

# 优势
* **自动化** WEB API 和 HTTP 前端
* 基于契约，统一前后端逻辑
* 基于业务，快速上手
* 学习成本低，几乎基于原生
* 模块化开发，随时集成


# 快速上手

1. 定义契约和模型
```cs
[Route("api/users")]
public interface IUserBusinessService : ICrudBusinessService<Guid, GetUserOutput, GetUserListOutput, UserCreateInput, UserUpdateInput>, IRemotingService //集成该接口实现自动化 WEB API
{
    [Get("by-name/{userName}")]
    Task<Return<GetSingleUserOutput>> GetByNameAsync(string userName);
}
```

2. 实现业务逻辑
```cs
public class UserBusinessService : CrudBusinessService<UserDbContext, Guid, GetUserOutput, GetUserListOutput, UserCreateInput, UserUpdateInput> , IUserBusinessService
{
    [Authorize]
    public async Task<Return<GetSingleUserOutput>> GetByNameAsync(string userName)
    {
        var user = Query.SingleOrDefault(m=>m.UserName == userName);
        if(user is null)
        {
            return Return<GetSingleUserOutput>.Failed("User is not found");
        }

        var model = Mapper.Map<User, GetSingleUserOutput>(user);
        return Return<GetSingleUserOutput>.Success(model);
    }
}
```

在 API 项目的 `Program.cs` 中
```cs
builder.Services.AddFocusOn(framework =>
{
    framework.AddSwagger(); //可选
    framework.AddAutoMapper(typeof(Program).Assembly);

    //自动化 WEB API
    framework.AddDynamicWebApi(typeof(IUserBusinessService).Assembly);
});
```

3. 在客户端层（例如 Blazor, Mvc 等)，配置 Service
```cs
builder.Service.AddFocusOn(configure =>
{
    configure.AddDynamicHttpProxy(typeof(IUserBusinessService).Assembly, options => options.BaseAddress = new("{API 地址}"));
});
```

使用 `IUserBusinessService`
```cs
var _userService = ServiceProfider.GetService<IUserBusinessService>();

var result = await _userService.GetByNameAsync("admin");
if(result.Succeed)
{
    var user = result.Data; //获取数据
}

//获取错误信息
result.Errors
```