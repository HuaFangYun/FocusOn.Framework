
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MiniSolution.HttpApi.Controllers
{
    //[ApiController]
    [Route("[controller]")]
    public abstract class ApiControllerBase:ControllerBase
    {
        protected IServiceProvider ServiceProvider => HttpContext.RequestServices;

        protected ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();
        protected virtual ILogger Logger => LoggerFactory.CreateLogger(GetType().Name);
    }
}
