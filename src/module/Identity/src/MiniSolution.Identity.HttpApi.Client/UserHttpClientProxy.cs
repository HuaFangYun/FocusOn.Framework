
using MiniSolution.ApplicationContracts.DTO;
using MiniSolution.HttpApi.Client;
using MiniSolution.Identity.ApplicationContracts.UserManagement;
using MiniSolution.Identity.ApplicationContracts.UserManagement.DTO;

namespace MiniSolution.Identity.HttpApi.Client;

public class UserHttpClientProxy<TKey> : UserHttpClientProxy<TKey, UserGetOutputDto<TKey>, UserGetListOutputDto<TKey>, UserGetListInputDto, UserCreateInputDto, UserUpdateInputDto>,IUserApplicationService<TKey>
{
    public UserHttpClientProxy(IServiceProvider services) : base(services)
    {
    }
}

public class UserHttpClientProxy<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
    : CrudHttpClientProxyBase<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>, IUserApplicationService<TKey, TGetOutputDto, TGetListOutputDto, TGetListInputDto, TCreateInputDto, TUpdateInputDto>
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
        var response= await Client.GetAsync(GetRequestUri($"username/{userName}"));
        return await GetOutputResultAsync<TGetOutputDto?>(response);
    }
}