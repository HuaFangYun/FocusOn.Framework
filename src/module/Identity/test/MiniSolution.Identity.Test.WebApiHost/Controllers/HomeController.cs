using Microsoft.AspNetCore.Mvc;

namespace MiniSolution.Identity.Test.WebApiHost.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
