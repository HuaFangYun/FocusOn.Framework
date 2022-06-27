using Microsoft.EntityFrameworkCore;

using MiniSolution.Endpoint.HttpApi.Test.WebHost.BusinessServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();

builder.Services.AddMiniSolution(configure =>
{
    configure.Services.AddDbContext<TestDbContext>(m=>m.UseInMemoryDatabase("test"));

    configure.AddBusinessService<ITestUserBusinessService, TestUserBusinessService>();
    configure.AddSwagger();
    configure.AddAutoMapper(typeof(Program).Assembly);
    configure.AddRemotingServiceHttpApi();
});

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseEndpoints(configure =>
{
    configure.MapDefaultControllerRoute();
    configure.MapSwagger();
});
app.Run();