using Application.Data;
using Domain.Entities.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

public static class DbHelperExtensions
{
	public static string CreateDbPath(this ConfigurationManager config, string section)
	{
		var connection = config.GetConnectionString(section);
		var sqliteBuilder = new SqliteConnectionStringBuilder(connection);
		sqliteBuilder.DataSource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appDB", sqliteBuilder.DataSource);
		if (!Directory.Exists(Path.GetDirectoryName(sqliteBuilder.DataSource)))
			Directory.CreateDirectory(Path.GetDirectoryName(sqliteBuilder.DataSource));

		return sqliteBuilder.ToString();
	}

	public static async Task DbInitAsync<T>(this IServiceProvider services) where T : DbContext
	{
		await using var scope = services.CreateAsyncScope();
		await using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		await context.Database.MigrateAsync();
	}

	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped(typeof(IRepository<>), typeof(AppDbRepository<>));
		foreach (var aggregateRootType in
				 typeof(IAggregateRoot).Assembly.GetExportedTypes()
					 .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
					 .ToList())
		{
			{
				services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
					sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));
			}
		}
		return services;
	}
}
