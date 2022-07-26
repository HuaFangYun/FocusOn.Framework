using Microsoft.AspNetCore.Mvc;
using FocusOn.Framework.AspNetCore.Http.Controllers;

namespace FocusOn.Framework.IntegrationTest.HttpApi.Controllers
{
    public class RoleController : ApiControllerBase
    {
        public RoleController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpGet]
        public IActionResult GetFile(string name)
        {
            return Ok();
        }
    }
}
