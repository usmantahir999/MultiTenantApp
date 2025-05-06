using Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Services.GetJwtSettings(builder.Configuration));

var app = builder.Build();
await app.Services.AddDatabaseInitializerAsync();


app.UseHttpsRedirection();
app.UseInfrastructure();
app.MapControllers();

app.Run();
