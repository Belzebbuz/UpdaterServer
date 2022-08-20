using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace UpdaterServer.AuthAttributes;

public class ClaimRequirementAttribute : AuthorizeAttribute, IAuthorizationFilter
{
	public string? Right { get; set; }
	public void OnAuthorization(AuthorizationFilterContext context)
	{
		if(Right != null)
		{
			var userRights = context.HttpContext.Items["rights"] as List<string>;
			if (userRights == null || !userRights.Contains(Right))
			{
				context.Result = new UnauthorizedResult();
			}
		}
	}
}

