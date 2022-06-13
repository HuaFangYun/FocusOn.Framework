using Boloni.Services.Abstractions;

namespace Boloni.HttpApi.Controllers.Abstrations
{
    public static class ControllerExtensions
    {
        public static IResult ToResult(this ApplicationResult result, int statusCode = 200) 
            => Results.Json(result, statusCode: statusCode);

        public static IResult ToResult<TResult>(this ApplicationResult<TResult> result, int statusCode = 200)
            => Results.Json(result, statusCode: statusCode);
    }
}
