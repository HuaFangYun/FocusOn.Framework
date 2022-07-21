using Microsoft.Extensions.DependencyInjection;
using FocusOn.Framework.Business.Service.TestHost.Services;

var service = new ServiceCollection();

service.AddFocusOn(builder =>
{
    builder.AddDynamicHttpProxy<ITestService>(config => config.BaseAddress = "http://localhost:5220/");
});

var provider = service.BuildServiceProvider();

var testService = provider.GetService<ITestService>();


var result = await testService.CreateAsync(new TestEntity() { Id = 1 });

Console.WriteLine(result.Succeed);