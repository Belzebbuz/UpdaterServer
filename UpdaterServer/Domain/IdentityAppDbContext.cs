using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Domain;

public class IdentityAppDbContext : IdentityDbContext
{
	public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options)
		: base(options)
	{ }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
        builder.ApplyConfiguration(new RoleConfiguration());
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
