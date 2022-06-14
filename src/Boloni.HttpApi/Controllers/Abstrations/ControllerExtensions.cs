using Boloni.DataTransfers;
using Boloni.Services.Abstractions;

namespace Boloni.HttpApi;

public static class ControllerExtensions
{
    public static IResult ToResult(this ApplicationResult result)
    {
        if (result.Succeed)
        {
            return Results.Ok();
        }
        return Results.BadRequest(result.Errors);
    }

    public static IResult ToResult<TResult>(this ApplicationResult<TResult> result)
    {
        if (result.Succeed)
        {
            return Results.Ok(result.Data);
        }
        return Results.BadRequest(result.Errors);
    }


    public static IResult ToResult(this OutputResult result, int statusCode = 200)
        => Results.Json(result, statusCode: statusCode);

    public static IResult ToResult<TResult>(this OutputModel<TResult> result, int statusCode = 200)
        => Results.Json(result, statusCode: statusCode);
}
