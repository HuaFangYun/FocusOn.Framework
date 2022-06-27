using FocusOn.Business.Contracts.DTO;
using FocusOn.Endpoints.HttpApi.Proxy;
using FocusOn.Identity.Business.Contracts.UserManagement;
using FocusOn.Identity.Business.Contracts.UserManagement.DTO;

namespace FocusOn.Identity.Endpoints.HttpApi.Client;

public class UserHttpClientProxy<TKey> : UserHttpClientProxy<TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>, IUserApplicationService<TKey>
{
    public UserHttpClientProxy(IServiceProvider services) : base(services)
    {
    }
}

public class UserHttpClientProxy<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    : CrudHttpClientProxy<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>, IUserApplicationService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    where TGetListInputDto : UserGetListInputDto
    where TGetListOutputDto : UserGetListOutputDto<TKey>
    where TGetOutputDto : UserGetOutputDto<TKey>
    where TCreateInputDto : UserCreateInputDto
    where TUpdateInputDto : UserUpdateInputDto
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