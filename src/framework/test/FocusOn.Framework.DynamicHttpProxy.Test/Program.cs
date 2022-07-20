using Microsoft.Extensions.DependencyInjection;
using FocusOn.Framework.Business.Service.TestHost.Services;

var service = new ServiceCollection();

service.AddFocusOn(builder =>
{
    builder.AddDynamicHttpProxy<ITestService>();
});

var provider = service.BuildServiceProvider();

var testService = provider.GetService<ITestService>();


var result = await testService.CreateAsync(new TestEntity());

Console.WriteLine(result.Succeed);