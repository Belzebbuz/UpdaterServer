using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace Infrastructure.Data;

public static class AuthExtensions
{
	public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
	{
		services.AddIdentity<AppUser, AppUserRole>(options =>
		{
			options.User.RequireUniqueEmail = true;
			options.Password.RequiredLength = 8;
			options.Password.RequireLowercase = false;
			options.Password.RequireUppercase = false;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireDigit = false;
		}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
		
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		   .AddJwtBearer(options =>
		   {
			   var key = Encoding.UTF8.GetBytes(configuration["JWT:key"]);
			   options.SaveToken = true;
			   options.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateIssuer = false,
				   ValidateAudience = false,
				   ValidateLifetime = true,
				   ValidateIssuerSigningKey = true,
				   ValidIssuer = configuration["JWT:Issuer"],
				   ValidAudience = configuration["JWT:Audience"],
				   IssuerSigningKey = new SymmetricSecurityKey(key),
				   ClockSkew = TimeSpan.Zero
			   };
		   });

		services.AddAuthorization(options =>
		{
			options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "Admin"));
		});
		return services;
	}
}
