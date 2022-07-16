using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;

using Microsoft.AspNetCore.Mvc;

namespace FocusOn.Framework.Endpoint.Test.HttpApi.ProxyHost.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ITestUserBusinessService userBusinessService)
        {
            UserBusinessService = userBusinessService;
        }

        public ITestUserBusinessService UserBusinessService { get; }

        public async Task<IActionResult> Default()
        {
            var result = await UserBusinessService.GetListAsync();
            return Ok(result);
        }
    }
}
