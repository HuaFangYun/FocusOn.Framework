using MiniSolution.Business.Services;
using MiniSolution.Identity.Test.WebApiHost;
using MiniSolution.Identity.Test.WebApiHost.Business.Services;
using Microsoft.EntityFrameworkCore;
using MiniSolution.Endpoints.HttpApi;

var builder=WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TestIdentityDbContext>(configure =>
{
    configure.UseInMemoryDatabase("Identity");
});
builder.Services.AddMiniSolution(configure =>
{
    configure.AddSwagger();
    configure.AddAutoMapper(typeof(Program).Assembly);
    configure.AddBusinessService<ITestUserApplicationService, TestUserApplicationService>();
    configure.AddAutoHttpApi();
});


var app= builder.Build();

app.MapDefaultControllerRoute();
app.UseSwagger();
app.UseSwaggerUI();


await app.RunAsync();