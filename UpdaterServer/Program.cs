global using UpdaterServer.Domain;
global using UpdaterServer.Domain.Enties;
global using MediatR;

using Microsoft.EntityFrameworkCore;
using UpdaterServer.Services.TcpServices;
using System.Text.Json.Serialization;



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
	string connectionstring = builder.CreateDbPath();

	options.UseSqlite(connectionstring);

	options.UseLazyLoadingProxies();
});

var app = builder.Build();

//app.Services.GetRequiredService<ITcpServerService>().Run();

await app.DbInit<AppDbContext>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
