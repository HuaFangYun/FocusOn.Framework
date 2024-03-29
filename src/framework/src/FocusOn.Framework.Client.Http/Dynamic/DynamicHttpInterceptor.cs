﻿using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using FocusOn.Framework.Business.Contract.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FocusOn.Framework.Client.Http.Dynamic;
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

    public ILogger? Logger => LoggerFactory?.CreateLogger("DynamicHttpProxy");


    DynamicHttpClientProxy<TService> DynamicHttpClientProxy => (DynamicHttpClientProxy<TService>)ServiceProvider.GetRequiredService(typeof(DynamicHttpClientProxy<>).MakeGenericType(typeof(TService)));


    /// <summary>
    /// 通过调用的方法，组装要发送的请求消息。
    /// </summary>
    /// <param name="invocation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    HttpRequestMessage CreateRequestMessage(IInvocation invocation)
    {
        var method = invocation.Method;
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
        var template = httpMethodAttribute.Template;

        if (!template.IsNullOrEmpty())
        {
            if (!template.StartsWith("/"))
            {
                pathBuilder.Append("/");
            }
            pathBuilder.Append(template);
        }

        request.Method = httpMethodAttribute.Method;

        //解析参数
        var parameters = method.GetParameters();
        var queryBuilder = new StringBuilder();
        for (int i = 0; i < parameters.Length; i++)
        {
            var param = parameters[i];
            var parameterType = GetParameterType(param, out var name);

            var value = invocation.GetArgumentValue(i);

            switch (parameterType)
            {
                case HttpParameterType.FromBody:
                    if (value is not null)
                    {
                        var json = JsonConvert.SerializeObject(value);
                        request.Content = new StringContent(json, Encoding.Default, "application/json");
                    }
                    break;
                case HttpParameterType.FromQuery:
                    if (value is null)
                    {
                        goto default;
                    }
                    if (param.ParameterType.IsClass)
                    {
                        foreach (var property in param.ParameterType.GetProperties())
                        {
                            if (!property.CanRead)
                            {
                                continue;
                            }

                            if (property.TryGetCustomAttribute<JsonPropertyAttribute>(out var jsonProperty))
                            {
                                name = jsonProperty.PropertyName;
                            }
                            else if (property.TryGetCustomAttribute<JsonPropertyNameAttribute>(out var jsonNameProperty))
                            {
                                name = jsonNameProperty.Name;
                            }
                            else
                            {
                                name = property.Name;
                            }

                            var propertyValue = property.GetValue(value);
                            if (propertyValue is not null)
                            {
                                queryBuilder.AppendFormat("{0}={1}", name, propertyValue);
                            }
                        }
                    }
                    else
                    {
                        queryBuilder.AppendFormat("{0}={1}", name, value);
                    }
                    break;
                case HttpParameterType.FromHeader:
                    if (value is not null)
                    {
                        request.Headers.Add(name, value.ToString());
                    }
                    break;
                case HttpParameterType.FromRoute:
                    var match = Regex.Match(pathBuilder.ToString(), @"{\w+}");
                    if (match.Success)
                    {
                        pathBuilder.Replace(match.Value, match.Result(value?.ToString()));
                    }
                    break;
                default:
                    break;
            }
        }

        var uriString = $"{pathBuilder}{(queryBuilder.Length > 0 ? $"?{queryBuilder}" : String.Empty)}";
        request.RequestUri = new(uriString, UriKind.Relative);

        return request;
    }


    public void InterceptSynchronous(IInvocation invocation)
    {
        Logger?.LogTrace($"调用方法：{invocation.Method.Name}");
        throw new NotSupportedException("要求返回值必须是 Task<Return> 或 Task<Return<TResult>> 类型");
    }

    public void InterceptAsynchronous(IInvocation invocation)
    {
        //返回值不是 Task<T> 走这个方法，例如 ValueTask


        //var returnType = invocation.Method.ReturnType;

        //var requestMessage = CreateRequestMessage(invocation.Method,invocation);
        //var result = DynamicHttpClientProxy.SendAsync(requestMessage, returnType);



        //invocation.ReturnValue = result;
        throw new NotSupportedException("要求返回值必须是 Task<Return> 或 Task<Return<TResult>> 类型");
    }

    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        //返回值是 Task<T> 走这个方法

        var requestMessage = CreateRequestMessage(invocation);
        var result = DynamicHttpClientProxy.SendAsync<TResult>(requestMessage);

        invocation.ReturnValue = result;

    }

    HttpParameterType GetParameterType(ParameterInfo parameter, out string parameterName)
    {
        parameterName = parameter.Name;

        if (parameter.TryGetCustomAttribute<HttpParameterAttribute>(out var parameterAttribute))
        {
            if (!parameterAttribute.Name.IsNullOrEmpty())
            {
                parameterName = parameterAttribute.Name;
            }
            return parameterAttribute.Type;
        }

        return HttpParameterType.FromRoute;
    }
}
