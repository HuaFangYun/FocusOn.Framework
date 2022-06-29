using FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices;

using Microsoft.AspNetCore.Mvc;

namespace FocusOn.Endpoints.Test.HttpApi.ProxyHost.Controllers
{
    public class HomeController:Controller
    {
        public HomeController(ITestUserBusinessService userBusinessService)
        {
            UserBusinessService = userBusinessService;
        }

        public ITestUserBusinessService UserBusinessService { get; }

        public async Task<IActionResult> Default()
        {
            var result = await UserBusinessService.GetByNameAsync("abc");
            return Ok(result);
        }
    }
}
