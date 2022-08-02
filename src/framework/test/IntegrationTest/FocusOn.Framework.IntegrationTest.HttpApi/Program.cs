using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.IntegrationTest.Service;
using FocusOn.Framework.IntegrationTest.Contract;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddFocusOn(configure =>
{
    configure.AddBusinessService<IUserCrudBusinessService, UserCrudBusinessService>();
    configure.AddAutoMapper(typeof(MapProfile).Assembly).AddSwagger();

    configure.AddDynamicWebApi(typeof(UserCrudBusinessService).Assembly);
    configure.Services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("db"));

    configure.AddCors();
});

var app = builder.Build();

app.UseStaticFiles();


app.UseFocusOn();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
});

app.Run();