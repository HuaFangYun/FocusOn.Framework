using FocusOn.Endpoint.HttpApi.Test.WebHost.BusinessServices;
using FocusOn.Endpoints.Test.HttpApi.ProxyHost.Proxies;

using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();

builder.Services.AddFocusOn(configure =>
{
    configure.Services.AddHttpClient(Options.DefaultName, client => client.BaseAddress = new("http://localhost:5237"));
    configure.AddHttpClientProxy<ITestUserBusinessService, UserHttpApiClientProxy>();    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
