﻿using FocusOn;
using FocusOn.Framework;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FocusOn.Framework.IntegrationTest.Contract;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddFocusOn(configure =>
    {
        configure.AddDynamicHttpProxy(typeof(IUserCrudBusinessService).Assembly, options =>
        {
            options.BaseAddress = "http://localhost:4700";
            options.DelegatingHandlers.Add(new(s =>
            {
                return new TestDelegatingHandler(s.GetRequiredService<ILogger<TestDelegatingHandler>>());
            }));
        })
        ;

    });
});

var app = builder.Build();


var userService = app.Services.GetRequiredService<IUserCrudBusinessService>();

Console.WriteLine("-----Create-----");

var createResult = await userService.CreateAsync(new FocusOn.Framework.Business.Contract.Identity.DTO.IdentityUserCreateInput
{
    UserName = "user" + new Random().Next()
});

Console.WriteLine(createResult.Errors.JoinString(","));
Console.WriteLine("Get data - UID:{0}，UserName:{1}", createResult.Data.Id, createResult.Data.UserName);
Console.WriteLine();
Console.WriteLine("--------Get List----------");
var result = await userService.GetListAsync(new FocusOn.Framework.Business.Contract.Identity.DTO.IdentityUserListSearchInput { UserName = "123" });

Console.WriteLine(result.Succeed);
foreach (var item in result.Data.Items)
{
    Console.WriteLine(item.UserName);
}

Console.WriteLine();
Console.WriteLine("--------Get By Id----------");
var singleResult = await userService.GetAsync(createResult.Data.Id);
Console.WriteLine(singleResult.Succeed);
Console.WriteLine(singleResult.Errors.JoinString(","));
Console.WriteLine("Get data - UID:{0}，UserName:{1}", singleResult.Data.Id, singleResult.Data.UserName);

Console.WriteLine();
Console.WriteLine("--------Update----------");
await userService.UpdateAsync(createResult.Data.Id, new FocusOn.Framework.Business.Contract.Identity.DTO.IdentityUserCreateInput
{
    UserName = "bbbbb"
});


Console.WriteLine();
Console.WriteLine("--------Get List----------");
result = await userService.GetListAsync();

Console.WriteLine(result.Succeed);
foreach (var item in result.Data.Items)
{
    Console.WriteLine(item.UserName);
}

Console.WriteLine();
Console.WriteLine("--------Delete----------");
Console.WriteLine("Id:{0}, UserName:{1}", singleResult.Data.Id, singleResult.Data.UserName);
await userService.DeleteAsync(singleResult.Data.Id);

Console.WriteLine();
Console.WriteLine("--------Get List----------");
result = await userService.GetListAsync();

Console.WriteLine(result.Succeed);
foreach (var item in result.Data.Items)
{
    Console.WriteLine(item.UserName);
}

var signInResult = await userService.SignInAsync("1111111111111111111111111111111111111111111111111111111111");


class TestDelegatingHandler : DelegatingHandler
{
    private readonly ILogger<TestDelegatingHandler> _logger;

    public TestDelegatingHandler(ILogger<TestDelegatingHandler> logger)
    {
        this._logger = logger;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Token", "abc");

        _logger.LogInformation("每一次请求都会走的方法");

        return base.SendAsync(request, cancellationToken);
    }
}