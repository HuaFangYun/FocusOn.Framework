
using Microsoft.AspNetCore.Mvc;

using MiniSolution.ApplicationContracts.DTO;

namespace MiniSolution.HttpApi.Controllers;

public static class ControllerExtensions
{
    public static IActionResult ToResult(this OutputResult result) => new OkObjectResult(result);

    public static IActionResult ToResult<TResult>(this OutputResult<TResult> result) => new OkObjectResult(result);
}
