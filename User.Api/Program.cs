using Users.Api.Extensions;
using Users.Api.Middlewares;
using Users.Application;
using Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddOpenApi();

builder.Services.AddPresentation();
builder.Services.AddUserApplication();
builder.Services.AddUserInfrastructure();

builder.Services.AddNpgsqlDataSource("bookstoreuser");

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.Run();

