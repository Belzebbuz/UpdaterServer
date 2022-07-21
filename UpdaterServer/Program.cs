using BeetleX.FastHttpApi.Hosting;
using PushFile.Messages.TcpServices;
using PushFile.Messages.TcpServices.TcpClient;
using UpdaterServer.Services.TcpServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITcpServerService, TcpServerService>();
builder.Services.AddTransient<ITcpClientFileSender, TcpClientFileSender>();
var app = builder.Build();

app.UseTcpServer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
