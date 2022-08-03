using System.Security.Claims;
using FocusOn.Framework.Security;
using Microsoft.EntityFrameworkCore;
using FocusOn.Framework.IntegrationTest.Service;
using FocusOn.Framework.IntegrationTest.Contract;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication("Test");
// Add services to the container.

builder.Services.AddFocusOn(configure =>
{
    configure.AddBusinessService<IUserCrudBusinessService, UserCrudBusinessService>()
    .AddBusinessService<IRoleCrudBusinessService, RoleCrudBusinessService>()
    .AddBusinessService<IAccountService, AccountService>()
    ;
    configure.AddAutoMapper(typeof(MapProfile).Assembly).AddSwagger(options =>
    {
        options.Title = "FocusOn.Test";
    });

    configure.AddDynamicWebApi(typeof(UserCrudBusinessService).Assembly).AddCurrentPrincipalAccessor();
    configure.Services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("db"));

    configure.AddCors();
});

var app = builder.Build();
app.UseStaticFiles();
app.UseFocusOn();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.Use((context, request) =>
//{
//    var currentPrincipal = context.RequestServices.GetRequiredService<ICurrentPrincipalAccessor>();

//    var identity = new ClaimsIdentity(new List<Claim> { new(ClaimTypes.Name, "admin") }, "test", ClaimTypes.Name, ClaimTypes.Role);

//    //currentPrincipal.CurrentPrincipal.AddIdentity(identity);
//    context.User = new(identity);
//    return request();
//});


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