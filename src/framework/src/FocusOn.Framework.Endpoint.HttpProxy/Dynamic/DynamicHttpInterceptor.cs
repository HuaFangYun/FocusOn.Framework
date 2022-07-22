using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using Castle.DynamicProxy;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using FocusOn.Framework.Business.Contract;
using Microsoft.Extensions.DependencyInjection;
using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.Endpoint.HttpProxy.Dynamic;
internal class DynamicHttpInterceptor<TService> : IAsyncInterceptor
    where TService : class
{
    public DynamicHttpInterceptor(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public IServiceProvider ServiceProvider { get; }

    TService RemotingService => ServiceProvider.GetRequiredService<TService>();

    public IHttpClientFactory HttpClientFactory { get; }

    protected ILoggerFactory? LoggerFactory => ServiceProvider.GetService<ILoggerFactory>();

    public ILogger? Logger => LoggerFactory?.CreateLogger(GetType().Name);


    DynamicHttpClientProxy<TService> DynamicHttpClientProxy => (DynamicHttpClientProxy<TService>)ServiceProvider.GetRequiredService(typeof(DynamicHttpClientProxy<>).MakeGenericType(typeof(TService)));


    /// <summary>
    /// 通过调用的方法，组装要发送的请求消息。
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    HttpRequestMessage CreateRequestMessage(MethodInfo method)
    {
        if (method is null)
        {
            throw new ArgumentNullException(nameof(method));
        }

        var request = new HttpRequestMessage();
        var serviceType = typeof(TService);
        if (!serviceType.TryGetCustomAttribute<RouteAttribute>(out var routeAttribute))
        {
            throw new InvalidOperationException($"接口必须设置 {nameof(RouteAttribute)} 特性，才可以使用自动代理");
        }

        var pathBuilder = new StringBuilder(routeAttribute.Template);


        //解析调用的方法

        if (!method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute))
        {
            throw new InvalidOperationException($"方法必须设置 {nameof(HttpMethodAttribute)} 特性，才可以使用自动代理");
        }
        if (!httpMethodAttribute.Template.IsNullOrEmpty())
        {
            //requestUriList.Add(httpMethodAttribute.Template);
        }

        request.Method = httpMethodAttribute.Method;


        //解析参数
        var parameters = method.GetParameters();
        var queryBuilder = new StringBuilder();
        foreach (var param in parameters)
        {
            if (!param.TryGetCustomAttribute<HttpParameterAttribute>(out var parameterAttribute) || (parameterAttribute != null && parameterAttribute.Type == HttpParameterType.FromRoute))
            {
                var match = Regex.Match(httpMethodAttribute.Template, @"^{\w+}$");
                if (match.Success)
                {
                    pathBuilder.Append(match.Result(param.RawDefaultValue?.ToString()));
                }
            }

            var name = param.Name;

            if (parameterAttribute is not null && !string.IsNullOrEmpty(parameterAttribute.Name))
            {
                name = parameterAttribute.Name;
            }

            Logger?.LogTrace($"Parameter Name:{name}");

            var value = param.RawDefaultValue;



            switch (parameterAttribute.Type)
            {
                case HttpParameterType.FromBody:
                    var json = JsonConvert.SerializeObject(value);
                    Logger?.LogTrace($"Parameter Value(Body):{json}");
                    request.Content = new StringContent(json);
                    break;
                case HttpParameterType.FromQuery:
                    queryBuilder.AppendFormat("{0}={1}", name, value);
                    break;
                case HttpParameterType.FromHeader:
                    request.Headers.Add(name, param.RawDefaultValue?.ToString());
                    break;
                default:
                    break;
            }
        }

        var uriString = $"{pathBuilder}{(queryBuilder.Length > 0 ? $"?{queryBuilder}" : String.Empty)}";
        Logger?.LogTrace("Request Uri:{0}", uriString);
        request.RequestUri = new(uriString, UriKind.Relative);

        return request;
    }


    protected virtual async Task<Return> GetResultAsync(Task task, Type resultType)
    {
        await task;
        var resultProperty = typeof(Task<>)
            .MakeGenericType(resultType)
            .GetProperty(nameof(Task<Return>.Result), BindingFlags.Instance | BindingFlags.Public);

        return (Return)resultProperty.GetValue(task);
    }

    public void InterceptSynchronous(IInvocation invocation)
    {
        Logger?.LogTrace($"调用方法：{invocation.Method.Name}");
        InterceptAsynchronous(invocation);
    }

    public void InterceptAsynchronous(IInvocation invocation)
    {
        var requestMessage = CreateRequestMessage(invocation.Method);
        invocation.ReturnValue = DynamicHttpClientProxy.SendAsync(requestMessage).Result;
    }

    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        var requestMessage = CreateRequestMessage(invocation.Method);
        invocation.ReturnValue = DynamicHttpClientProxy.SendAsync<TResult>(requestMessage).Result;
    }
}
