using MiniSolution.Business.Contracts.DTO;
using MiniSolution.Endpoints.HttpApi.Proxy;
using MiniSolution.Identity.Business.Contracts.UserManagement;
using MiniSolution.Identity.Business.Contracts.UserManagement.DTO;

namespace MiniSolution.Identity.Endpoints.HttpApi.Client;

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

    protected override string RootPath => "users";

    public virtual async Task<OutputResult<TGetOutputDto?>> GetByUserNameAsync(string userName)
    {
        var response = await Client.GetAsync(GetRequestUri($"username/{userName}"));
        return await GetOutputResultAsync<TGetOutputDto?>(response);
    }
}