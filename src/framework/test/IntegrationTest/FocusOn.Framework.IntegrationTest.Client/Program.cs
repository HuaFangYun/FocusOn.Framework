using FocusOn.Framework.IntegrationTest.Contract;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddFocusOn(configure =>
    {
        configure.AddDynamicHttpProxy<IUserCrudBusinessService>(options =>
        {
            options.BaseAddress = "http://localhost:4700";
        });
    });
});

var app = builder.Build();


var userService = app.Services.GetRequiredService<IUserCrudBusinessService>();

var result = await userService.GetListAsync();

Console.WriteLine(result.Succeed);
foreach (var item in result.Data.Items)
{
    Console.WriteLine(item.UserName);
}