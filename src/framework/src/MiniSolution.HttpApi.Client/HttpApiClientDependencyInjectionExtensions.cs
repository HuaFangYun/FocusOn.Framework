using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using MiniSolution.ApplicationContracts;

namespace MiniSolution.HttpApi.Client;

public static class HttpApiClientDependencyInjectionExtensions
{
    public static MiniSolutionBuilder AddHttpClientProxy<TService, TClientProxy>(this MiniSolutionBuilder builder)
        where TService : class, IApplicationSerivce
        where TClientProxy : class, IClientProxy, TService
    {
        builder.Services.AddScoped<TService, TClientProxy>();
        return builder;
    }
}
