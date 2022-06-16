
using Microsoft.AspNetCore.Mvc;

namespace MiniSolution.HttpApi.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }
}