using MiniSolution.HttpApi.Client;
using MiniSolution.Identity.ApplicationContracts.UserManagement;
using MiniSolution.Identity.HttpApi.Client;

var builder=WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddHttpClient(Const.HttpClientName, configure => configure.BaseAddress = new("https://localhost:57780"));
builder.Services.AddMiniSolution(configure =>
{
    //configure.Services.AddTransient(typeof(IUserApplicationService<>), typeof(UserHttpClientProxy<>));
    configure.AddHttpClientProxy<IUserApplicationService<Guid>, UserHttpClientProxy<Guid>>();
});

var app=builder.Build();
app.MapDefaultControllerRoute();
app.Run();