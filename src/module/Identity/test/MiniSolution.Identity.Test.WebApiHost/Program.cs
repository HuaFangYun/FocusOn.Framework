using MiniSolution.ApplicationServices;
using MiniSolution.HttpApi;
using MiniSolution.Identity.Test.WebApiHost;
using MiniSolution.Identity.Test.WebApiHost.ApplicationServices;
using Microsoft.EntityFrameworkCore; 
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
    configure.AddApplicationService<ITestUserApplicationService, TestUserApplicationService>();
});


var app= builder.Build();

app.MapDefaultControllerRoute();
app.UseSwagger();
app.UseSwaggerUI();


await app.RunAsync();