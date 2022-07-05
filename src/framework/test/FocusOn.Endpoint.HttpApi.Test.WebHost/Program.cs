using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddFocusOn(configure =>
{
    configure.Services.AddDbContext<TestDbContext>(m => m.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FocusOn;Trusted_Connection=True;MultipleActiveResultSets=true"));

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