# FocusOn.Framework
基于契约的轻量化前后端全栈应用框架。

# 优势
* 让开发者聚焦于前后端的应用开发，而不是框架
* TA 是很轻量级的，只有前后端三层
* 基于契约，前后端共用一个方法声明，避免重复定义
* 自动化处理，后端 API 和前端的 HTTP Client 由底层框架处理，无需操心
* 集成了 `AutoMapper` 来处理对象映射

# 劣势
* 仅支持 EF
* 仅支持 HTTP WEB API
* 目标是企业级应用


# 快速上手

1. 定义契约
```cs
public interface IUserBusinessService : ICrudBusinessService<Guid, GetUserOutput, GetUserListOutput, UserCreateInput, UserUpdateInput>
{
    Task<OutputResult<GetSingleUserOutput>> GetByNameAsync(string userName);
}
```

2. 直接上手 Controller
```cs
[Route("users")]
public class UserApiController : CrudApiControllerBase<UserDbContext, Guid, GetUserOutput, GetUserListOutput, UserCreateInput, UserUpdateInput> , IUserBusinessService
{
    [HttpGet("by-name/{userName}")]
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

`Program.cs` 中
```cs
builder.Services.AddFocusOn(configure =>
{
    configure.AddFramework(framework =>{
        framework.AddSwagger(); //可选
        framework.AddAutoMapper(typeof(Program).Assembly);
    });
});
```

3. 前端代理实现
```cs
public class UserHttpApiClientProxy : CrudHttpApiClientProxy<User, Guid, GetUserOutput, GetUserListOutput, UserCreateInput, UserUpdateInput>, IUserBusinessService
{
    public UserHttpApiClientProxy(IServiceProvider serviceProvider):base(serviceProvider)
    {

    }

    protected override string RootPath => "users";

    public async Task<OutputResult<GetSingleUserOutput>> GetByNameAsync(string userName)
    {
        var url = GetRequestUri($"by-name/{name}");
        var result = await Client.GetAsync(url);
        return await HandleOutputResultAsync<User>(result);
    }
}
```

在客户端层（例如 Blazor)，配置 Service
```cs
builder.Service.AddFocusOn(configure =>
{
    configure.AddHttpClient(options => options.BaseAddress = builder.HostEnvironment.BaseAddress);
    configure.AddHttpProxy<IUserBusinessService, UserHttpApiClientProxy>();
});
```

在客户都使用 `IUserBusinessService` 调用
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