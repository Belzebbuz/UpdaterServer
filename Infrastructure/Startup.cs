using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
		{
			string connectionstring = configuration.CreateDbPath("DefaultConnection");
			options.UseSqlite(connectionstring);
		});
		services.AddAuth(configuration);
		services.AddRepositories();
		return services;
    }
}
