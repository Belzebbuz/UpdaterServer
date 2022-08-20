global using MediatR;
global using Domain.Entities;
global using Application.DTO.Common;
global using Application.Extensions;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Configuration;
global using Application.Data;
using Microsoft.Extensions.DependencyInjection;
using Application.Services.Common;

namespace Application;

public static class Startup
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(typeof(Startup).Assembly);
		services.AddTransient<IEmailSender, EmailSender>();
		return services;
	}
}
