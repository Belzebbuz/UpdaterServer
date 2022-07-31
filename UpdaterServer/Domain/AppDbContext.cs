using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UpdaterServer.Domain;

public class AppDbContext : IdentityDbContext
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
		modelBuilder.ApplyConfiguration(new RoleConfiguration());
	}
}
public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
	public void Configure(EntityTypeBuilder<IdentityRole> builder)
	{
		builder.HasData(
			new IdentityRole
			{
				Name = "Viewer",
				NormalizedName = "VIEWER"
			},
			new IdentityRole
			{
				Name = "Admin",
				NormalizedName = "ADMIN"
			}
			,
			new IdentityRole
			{
				Name = "Dev",
				NormalizedName = "DEV"
			}
		);
	}
}