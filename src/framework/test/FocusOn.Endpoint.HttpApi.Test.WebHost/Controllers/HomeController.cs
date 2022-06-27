using Microsoft.AspNetCore.Mvc;

namespace FocusOn.Endpoint.HttpApi.Test.WebHost.Controllers
{
    public class HomeController:Controller

    {

        public IActionResult Index() => Redirect("~/Swagger");
    }
}
