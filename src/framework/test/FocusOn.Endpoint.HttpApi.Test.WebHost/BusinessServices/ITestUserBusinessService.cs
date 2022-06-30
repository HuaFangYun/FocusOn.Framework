
using FocusOn.Business.Contracts;
using FocusOn.Business.Contracts.DTO;
using FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices;

public interface ITestUserBusinessService:ICrudBusinessService<Guid,User>
{
    Task<OutputResult<User>> GetByNameAsync(string name);
}
