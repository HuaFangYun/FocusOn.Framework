
using Microsoft.AspNetCore.Mvc;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.Controllers;

public class HomeController : Controller

{

    public IActionResult Index() => Redirect("~/Swagger");
}
