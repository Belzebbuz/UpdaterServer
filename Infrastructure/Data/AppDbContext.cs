using Domain.Entities;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class AppDbContext : IdentityDbContext<AppUser, AppUserRole, string>
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{

	}

	public DbSet<Project> Projects => Set<Project>();
	public DbSet<ReleaseAssembly> ReleaseAssemblies => Set<ReleaseAssembly>();
	public DbSet<PatchNote> PatchNotes => Set<PatchNote>();
	public DbSet<UserRight> UserRights => Set<UserRight>();
	public DbSet<UserGroup> UserGroup => Set<UserGroup>();
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
