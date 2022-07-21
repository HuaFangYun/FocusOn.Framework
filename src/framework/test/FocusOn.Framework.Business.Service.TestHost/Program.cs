using FocusOn.Framework.Business.Service.TestHost.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFocusOn(configure =>
{
    configure.AddAutoWebApi();
    configure.AddSwagger().AddAutoMapper(typeof(Program).Assembly);
    configure.AddBusinessService<ITestService, TestService>();
    configure.Services.AddDbContext<TestDbContext>();
});

builder.Services.AddCors(options => options.AddPolicy("All", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseStaticFiles();
app.UseSwagger().UseSwaggerUI();
app.UseRouting();
app.UseCors();
// Configure the HTTP request pipeline.
app.MapGet("", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.UseEndpoints(configure =>
{
    configure.MapControllers();
    //configure.MapSwagger();
});

app.Run();