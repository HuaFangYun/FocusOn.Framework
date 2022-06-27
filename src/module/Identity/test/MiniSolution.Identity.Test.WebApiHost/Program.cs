using FocusOn.Business.Services;
using FocusOn.Identity.Test.WebApiHost;
using FocusOn.Identity.Test.WebApiHost.Business.Services;
using Microsoft.EntityFrameworkCore;
using FocusOn.Endpoints.HttpApi;

var builder=WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TestIdentityDbContext>(configure =>
{
    configure.UseInMemoryDatabase("Identity");
});
builder.Services.AddFocusOn(configure =>
{
    configure.AddSwagger();
    configure.AddAutoMapper(typeof(Program).Assembly);
    configure.AddBusinessService<ITestUserApplicationService, TestUserApplicationService>();
    configure.AddRemotingServiceHttpApi();
});


var app= builder.Build();

app.MapDefaultControllerRoute();
app.UseSwagger();
app.UseSwaggerUI();


await app.RunAsync();