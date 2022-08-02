using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FocusOn.Framework.AspNetCore.Http.Controllers;

namespace FocusOn.Framework.IntegrationTest.HttpApi.Controllers
{
    [Authorize]
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
