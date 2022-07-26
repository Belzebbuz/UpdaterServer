global using UpdaterServer.Domain;
global using UpdaterServer.Domain.Enties;
global using MediatR;
global using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using UpdaterServer.Services.TcpServices;
using System.Text.Json.Serialization;
using UpdaterServer.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITcpServerService, TcpServerService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
	string connectionstring = builder.CreateDbPath("DefaultConnection");
	options.UseSqlite(connectionstring);
	options.UseLazyLoadingProxies();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuth(builder);
var app = builder.Build();

//app.Services.GetRequiredService<ITcpServerService>().Run();

await app.DbInit<AppDbContext>();
await app.DbInit<IdentityAppDbContext>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
