using Boloni.Data;
using Boloni.Data.Entities;
using Boloni.DataTransfers;
using Boloni.DataTransfers.Localizations;
using Boloni.DataTransfers.Users;
using Boloni.HttpApi.Controllers.Abstrations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boloni.HttpApi.Users;
[Route("api/users")]
public class UserApiController : BoloniCrudControllerBase<BoloniDbContext, User, Guid,GetUserOutputDto,GetUserListOutputDto,GetUserListInputDto,CreateUserInputDto,UpdateUserInputDto>
{

    public UserApiController()
    {
    }

    protected IPasswordHasher PasswordHasher => ServiceProvider.GetRequiredService<IPasswordHasher>();

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(OutputModel<>))]
    [ProducesResponseType(400, Type = typeof(OutputResult))]
    public override async Task<IResult> CreateAsync([FromBody] CreateUserInputDto model)
    {
        var valid = await Query.AnyAsync(CancellationToken);
        if (valid)
        {
            return OutputResult.Failed(Locale.Message_UserNameDuplicate).ToResult(400);
        }

        var entity = Mapper.Map<CreateUserInputDto, User>(model);

        var hashedPassword = PasswordHasher.HashPassword(model.Password);
        entity.SetPassword(hashedPassword);

        await SaveChangesAsync();

        return OutputModel<Guid>.Success(entity.Id).ToResult();
    }

    [HttpGet("test")]
    public IResult Test()
    {
        return Results.Ok(new {a=1});
    }
}
