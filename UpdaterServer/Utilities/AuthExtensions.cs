using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UpdaterServer.DTOs;

namespace UpdaterServer.Utilities
{
	public static class AuthExtensions
	{
		public static IServiceCollection AddAuth(this IServiceCollection services, WebApplicationBuilder builder)
		{
			services.AddIdentity<IdentityUser, IdentityRole>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
			services.AddDbContext<AppDbContext>(options =>
			{
				string connectionstring = builder.CreateDbPath("IdentityConnection");
				options.UseSqlite(connectionstring);
			});
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			   .AddJwtBearer(options =>
			   {
				   var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]);
				   options.SaveToken = true;
				   options.TokenValidationParameters = new TokenValidationParameters
				   {
					   ValidateIssuer = false,
					   ValidateAudience = false,
					   ValidateLifetime = true,
					   ValidateIssuerSigningKey = true,
					   ValidIssuer = builder.Configuration["JWT:Issuer"],
					   ValidAudience = builder.Configuration["JWT:Audience"],
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

		public static IEnumerable<Claim> ParseClaimsFromJwt(this string jwt)
		{
			var claims = new List<Claim>();
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
			ExtractRolesFromJWT(claims, keyValuePairs);
			claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
			return claims;
		}
		private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
		{
			keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
			if (roles != null)
			{
				var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');
				if (parsedRoles.Length > 1)
				{
					foreach (var parsedRole in parsedRoles)
					{
						claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));
					}
				}
				else
				{
					claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
				}
				keyValuePairs.Remove(ClaimTypes.Role);
			}
		}

		private static byte[] ParseBase64WithoutPadding(string base64)
		{
			switch (base64.Length % 4)
			{
				case 2: base64 += "=="; break;
				case 3: base64 += "="; break;
			}
			return Convert.FromBase64String(base64);
		}
	}
}
