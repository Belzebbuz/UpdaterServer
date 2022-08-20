using Ardalis.Specification;
using Domain.Entities.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Extensions;

public static class Auth
{
	public static async Task<(string Token, DateTime Expiration)> BuildTokenAsync(this UserManager<AppUser> userManager, IReadRepository<AppUser> readRepository, IConfiguration configuration, string email)
	{
		var claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Email, email)
		};

		var user = await userManager.FindByNameAsync(email);
		var claimsDB = await userManager.GetClaimsAsync(user);
		claims.AddRange(claimsDB);

		var userDb = await readRepository.FirstOrDefaultAsync(new FindUserByIdSpec(user.Id));
		var rights = new List<string>();
		user.UserGroups.ForEach(x => x.Roles.ForEach(x => x.UserRights.ForEach(x => rights.Add(x.Name))));


		foreach (var right in rights)
		{
			claims.Add(new Claim(ClaimTypes.Role, right));
		}
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var expiration = DateTime.UtcNow.AddYears(1);

		var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
			expires: expiration, signingCredentials: creds);

		return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
	}

	public class FindUserByIdSpec : Specification<AppUser>, ISingleResultSpecification
	{
		public FindUserByIdSpec(string id) =>
				Query.Where(x => x.Id == id)
					.Include(x => x.UserGroups)
					.ThenInclude(x => x.Roles)
					.ThenInclude(x => x.UserRights);
	}
}
