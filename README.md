# MiniSolution
基于契约的全栈开发框架，由框架自动生成 HTTP API 代理和 HTTP WEB API 终结点，开发者只需要集中精力在业务逻辑上即可。以沿用原生 .NET 体系技术为原则，让学习门槛降低约至 0，并支持模块化开发的功能，具备无限扩展的能力。用最少的时间开发出最接近需求的产品。

# 架构图

# 优势

* 前后端分离经常会在联调时产生各种问题。但在 .NET 体系下，因为 Blazor 的诞生，让全栈不是梦。因此只要基于契约，让框架实现前后端自动化处理即可，减少因为前后端各种不一致造成的问题。
* 简单易学。我们保证基于原生技术进行功能扩展，尽可能减少新框架的学习成本，只需要懂三层架构即可。
* 支持 EF Core（目前），开箱即用

# 快速上手

1. 定义契约
```cs
public interface IUserService : ICrudBusinessService<Guid, GetUserOutput, GetUserListOutput, UserCreateInput, UserUpdateInput>
{
    Task<OutputResult<GetSingleUserOutput>> GetByNameAsync(string userName);
}
```

2. 后端业务实现
```cs
public class UserService : EfCoreBusinessService<UserDbContext, Guid, GetUserOutput, GetUserListOutput, UserCreateInput, UserUpdateInput> , IRemotingService
{
    public UserService(IServiceProvider serviceProvider):base(serviceProvider)
    {

    }

    public async Task<OutputResult<GetSingleUserOutput>> GetByNameAsync(string userName)
    {
        var user = Query.SingleOrDefault(m=>m.UserName == userName);
        if(user is null)
        {
            return OutputResult<GetSingleUserOutput>.Failed("User is not found");
        }

        var model = Mapper.Map<User, GetSingleUserOutput>(user);
        return OutputResult<GetSingleUserOutput>.Success(model);
    }
}
```
继承了 `IRemotingService` 会自动生成 web api

因此该方法会生成 `/api/users/byName?userName={userName}` 格式的路由

在 API 层，配置 Service
```cs
...
builder.Services.AddMiniSolution(configure =>
{
    configure.AddSwagger();
    configure.AddBusinessService<IUserService, UserService>();
});
...
```
然后运行 API，或 Swagger

3. 前端代理实现
```cs
public class UserServiceHttpProxy : CrudHttpProxy<User, Guid, GetUserOutput, GetUserListOutput, UserCreateInput, UserUpdateInput>
{
    public UserServiceHttpProxy(IServiceProvider serviceProvider):base(serviceProvider)
    {

    }

    public async Task<OutputResult<GetSingleUserOutput>> GetByNameAsync(string userName)
    {
        var result = Client.GetAsync(GetRequestUri(userName));
        return GetOutputResult<GetSingleUserOutput>(result);
    }
}
```

在客户端层（例如 Blazor)，配置 Service
```cs
builder.Service.AddMiniSolution(configure =>
{
    configure.AddHttpProxy<IUserService, UserServiceHttpProxy>();
});
```

在客户都使用 `IUserService` 调用
```cs
var _userService = ServiceProfider.GetService<IUserService>();

var result = await _userService.GetByNameAsync("admin");
if(result.Succeed)
{
    var user = result.Data; //获取数据
}

//获取错误信息
result.Errors
```