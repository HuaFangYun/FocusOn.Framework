using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Boloni.Services.Abstractions;

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
