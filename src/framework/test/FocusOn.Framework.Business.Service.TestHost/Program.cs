using FocusOn.Framework.Business.Service.TestHost.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFocusOn(configure =>
{
    configure.AddAutoWebApi();
    configure.AddSwagger();
    configure.AddBusinessService<ITestService, TestService>();
});

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();
// Configure the HTTP request pipeline.
app.MapGet("", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.Run();