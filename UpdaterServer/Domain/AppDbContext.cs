using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdaterServer.Domain.Enties;

namespace UpdaterServer.Domain
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{

		}
		public DbSet<Project> Projects => Set<Project>();
		public DbSet<ReleaseAssembly> ReleaseAssemblies => Set<ReleaseAssembly>();
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
