using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;
using System.IO.Compression;
using System.Security.Claims;
using UpdaterServer.Messages.Apps;
using UpdaterServer.Messages.ReleaseAssemblies;


namespace UpdaterServer.Controllers;

[Route("api/[controller]")]
public class AppsController : BaseApiController
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AppsController(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}
	[HttpGet]
	public async Task<IActionResult> GetAllAppsAsync()
	{
		var apps = await Mediator.Send(new GetAllAppsRequest());
		if (apps.Any())
		{
			return Ok(apps);
		}
		else
		{
			return NotFound("App's table is empty");
		}
	}

	[HttpGet("{id}")]
	public async Task<Project> GetProjectAsync(Guid id) => await Mediator.Send(new GetProjectByIdRequest(id));

	[HttpGet("filter/{isWinService}")]
	public async Task<IEnumerable<Project>> GetAppsByWinIsWinService(bool isWinService) =>
		await Mediator.Send(new GetAppsByFilterRequest { IsWinService = isWinService, Name = String.Empty });

	[HttpGet("{isWinService}/{name}")]
	public async Task<IEnumerable<Project>> GetAppsByFilter(bool isWinService, string name) =>
		await Mediator.Send(new GetAppsByFilterRequest { IsWinService = isWinService, Name = name });

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Dev")]
	public async Task<IActionResult> CreateAppAsync(CreateAppRequest app)
	{
		var Id = await Mediator.Send(app);
		if (Id == null)
			return BadRequest("An application with the same name already exists");

		return Ok(Id);
	}

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Dev")]
	public async Task<IActionResult> UpdateAppInfoAsync(UpdateAppRequest request)
	{
		var newApp = await Mediator.Send(request);
		if (newApp == null)
			return NotFound($"App with Id = {request.Id} not found!");

		return Ok();
	}

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Dev")]
	public async Task<IActionResult> DeleteAppAsync(Guid id)
	{
		var success = await Mediator.Send(new DeleteAppRequest(id));
		if (success)
			return Ok();
		else
			return BadRequest();
	}
}
