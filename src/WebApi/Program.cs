using Infrastructure;
using Application;
using WebApi;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Services.GetJwtSettings(builder.Configuration));
builder.Services.AddApplicationServices();
var app = builder.Build();
await app.Services.AddDatabaseInitializerAsync();


app.UseHttpsRedirection();
app.UseInfrastructure();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.Run();
