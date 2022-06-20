
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MiniSolution.Business.Contracts;

public abstract class ApplicationServiceBase : IApplicationSerivce
{

    public ApplicationServiceBase(IServiceProvider services)
    {
        Services = services;
    }
    public IServiceProvider Services { get; }

    protected ILoggerFactory LoggerFactory => Services.GetRequiredService<ILoggerFactory>();

    protected ILogger Logger => LoggerFactory.CreateLogger(GetType().Name);
}
