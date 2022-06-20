using Microsoft.AspNetCore.Mvc;

using MiniSolution.Identity.ApplicationContracts.UserManagement;

namespace MiniSolution.Identity.Test.ClientWeb.Controllers;

public class HomeController:Controller
{
    public HomeController(IUserApplicationService<Guid> userApplicationService)
    {
        UserApplicationService = userApplicationService;
    }

    public IUserApplicationService<Guid> UserApplicationService { get; }

    public async Task<IActionResult> Index()
    {
        var result = await UserApplicationService.GetByUserNameAsync("abc");
        return Json(result);
    }
}
