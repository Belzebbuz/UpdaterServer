using BeetleX.FastHttpApi.Hosting;
using Microsoft.EntityFrameworkCore;
using UpdaterServer.Domain;
using System.Linq;
using UpdaterServer.Services.TcpServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITcpServerService, TcpServerService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
	var connection = builder.Configuration.GetConnectionString("DefaultConnection");
	options.UseSqlite(connection);
});

var app = builder.Build();
app.Services.GetRequiredService<ITcpServerService>().Run();
using (var scope = app.Services.CreateScope())
{
	
	using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
