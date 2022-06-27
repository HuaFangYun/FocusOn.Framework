using FocusOn.Business.Contracts.DTO;
using FocusOn.Endpoints.HttpApi.Proxy;
using FocusOn.Identity.Business.Contracts.UserManagement;
using FocusOn.Identity.Business.Contracts.UserManagement.DTO;

namespace FocusOn.Identity.Endpoints.HttpApi.Client;

public class UserHttpClientProxy<TKey> : UserHttpClientProxy<TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>, IUserBusinessService<TKey>
{
    public UserHttpClientProxy(IServiceProvider services) : base(services)
    {
    }
}

public class UserHttpClientProxy<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    : CrudHttpClientProxy<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>, IUserBusinessService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    where TGetListInputDto : class
    where TGetListOutputDto : class
    where TGetOutputDto : class
    where TCreateInputDto : class
    where TUpdateInputDto : class
{
    public UserHttpClientProxy(IServiceProvider services) : base(services)
    {
    }

    protected override string? Name => Const.HttpClientName;

    protected override string RootPath => "api/users";

    public virtual async Task<OutputResult<TGetOutputDto?>> GetByUserNameAsync(string userName)
    {
        var response = await Client.GetAsync(GetRequestUri($"username/{userName}"));
        return await GetOutputResultAsync<TGetOutputDto?>(response);
    }
}