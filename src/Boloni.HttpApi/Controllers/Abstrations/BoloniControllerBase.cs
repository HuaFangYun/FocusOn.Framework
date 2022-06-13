
using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace Boloni.HttpApi.Controllers.Abstrations;
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class BoloniControllerBase : ControllerBase
{

    protected IServiceProvider ServiceProvider => Request.HttpContext.RequestServices;

    protected ILoggerFactory LoggerFactory=>ServiceProvider.GetRequiredService<ILoggerFactory>();
    protected ILogger Logger => LoggerFactory.CreateLogger(GetType().Name);

    protected IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();
}
