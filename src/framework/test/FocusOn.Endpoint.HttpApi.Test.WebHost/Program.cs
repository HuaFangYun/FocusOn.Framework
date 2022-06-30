using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddFocusOn(configure =>
{
    configure.Services.AddDbContext<TestDbContext>(m=>m.UseInMemoryDatabase("test"));

    configure.AddSwagger();
    configure.AddAutoMapper(typeof(Program).Assembly);
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