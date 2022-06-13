using Boloni.Data.Entities;
using Boloni.DataTransfers;
using Boloni.DataTransfers.Localizations;
using Boloni.DataTransfers.Users;
using Boloni.HttpApi.Controllers.Abstrations;
using Boloni.Services.Abstractions;
using Boloni.Services.Users;

using Microsoft.AspNetCore.Mvc;

namespace Boloni.HttpApi.Controllers;
[Route("api/users")]
public class UserApiController : BoloniControllerBase
{
    private readonly UserAppService _userAppService;

    public UserApiController(UserAppService userAppService)
    {
        this._userAppService = userAppService;
    }

    [HttpPost]
    [ProducesResponseType(200,Type =typeof(OutputModel<>))]
    [ProducesResponseType(400,Type= typeof(OutputModel))]
    public async Task<IResult> CreateUserAsync([FromBody]CreateUserInputDto model)
    {
        var valid = await _userAppService.GetUserNameExists(model.UserName);
        if (valid)
        {
            return ApplicationResult.Failed(Locale.Message_UserNameDuplicate).ToResult(400);
        }

        var entity=Mapper.Map<CreateUserInputDto,User>(model);
        var result = await _userAppService.CreateAsync(entity, model.Password);
        return result.ToResult();
    }
}
