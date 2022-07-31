﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Domain
{
	public static class DBHelperExtensions
	{
		public static string CreateDbPath(this WebApplicationBuilder builder, string section)
		{
			var connection = builder.Configuration.GetConnectionString(section);
			var sqliteBuilder = new SqliteConnectionStringBuilder(connection);
			sqliteBuilder.DataSource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appDB", sqliteBuilder.DataSource);
			if (!Directory.Exists(Path.GetDirectoryName(sqliteBuilder.DataSource)))
				Directory.CreateDirectory(Path.GetDirectoryName(sqliteBuilder.DataSource));

			var connectionstring = sqliteBuilder.ToString();
			return connectionstring;
		}

		public static async Task DbInitAsync<T>(this WebApplication app) where T : DbContext
		{
			using (var scope = app.Services.CreateScope())
			{

				using var context = scope.ServiceProvider.GetRequiredService<T>();
				await context.Database.MigrateAsync();
			}
		}
	}
}
