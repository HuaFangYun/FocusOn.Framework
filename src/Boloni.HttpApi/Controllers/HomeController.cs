using Microsoft.AspNetCore.Mvc;

namespace Boloni.HttpApi.Controllers;

public class HomeController:Controller
{
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
