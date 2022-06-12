using Microsoft.AspNetCore.Mvc;

namespace Boloni.HttpApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public abstract class BoloniControllerBase : ControllerBase
{
}
