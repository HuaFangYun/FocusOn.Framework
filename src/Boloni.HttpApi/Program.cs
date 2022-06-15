using Boloni.DataTransfers.Users;
using Boloni.HttpApi;
using Boloni.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

//app.MapControllers();

app.MapCrud<UserAppService,Guid, GetUserOutputDto, GetUserListOutputDto, GetUserListInputDto, CreateUserInputDto, UpdateUserInputDto>("users","User Manage");

app.Run();
