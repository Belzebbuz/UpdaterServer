using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using UpdaterServer.Utilities;

namespace UpdaterServer.Middlewares;

public class JwtMiddleware
{
	private readonly RequestDelegate _next;

	public JwtMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context, AppDbContext appDbContext)
	{
		var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
		if (token != null)
		{
			var claims = token.ParseClaimsFromJwt();
			if (claims != null)
			{
				var userEmail = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
				var user = await appDbContext.Users
					.Where(x => x.Email == userEmail.Value)
					.Include(x => x.UserGroups)
					.ThenInclude(x => x.Roles)
					.ThenInclude(x => x.UserRights)
					.FirstOrDefaultAsync();
				List<string> rights = new();
				user.UserGroups.ForEach(x => x.Roles.ForEach(x => x.UserRights.ForEach(x => rights.Add(x.Name))));
				context.Items["rights"] = rights;
			}
		}

		await _next(context);
	}
}
