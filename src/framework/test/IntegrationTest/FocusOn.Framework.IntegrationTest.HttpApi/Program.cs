using FocusOn.Framework.IntegrationTest.Contract;
using FocusOn.Framework.IntegrationTest.Service;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddFocusOn(configure =>
{
    configure.AddBusinessService<IUserCrudBusinessService, UserCrudBusinessService>();
    configure.AddAutoMapper(typeof(MapProfile).Assembly).AddSwagger();

    configure.AddDynamicWebApi(typeof(UserCrudBusinessService).Assembly);
    configure.Services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("db"));
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSwagger().UseSwaggerUI();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapSwagger();

    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
});

app.Run();