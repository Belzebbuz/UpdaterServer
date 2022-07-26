using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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

	[HttpPost("{id}/addRelease")]
	[RequestFormLimits(MultipartBodyLengthLimit = 1_000_524_288_000)]
	[RequestSizeLimit(1_000_524_288_000)]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Dev")]
	public async Task<IActionResult> CreateNewReleaseAsync(Guid Id, [FromForm] IFormFile file)
	{
		var exist = await Mediator.Send(new AppExistRequest(Id));
		if (!exist)
			return NotFound($"Application with Id = {Id} not found");

		string filePath = Path.Combine(ReleasesPath, $"{Id}{Path.GetExtension(file.FileName)}");
		await using (var fs = System.IO.File.Create(filePath))
		{
			await file.CopyToAsync(fs);
			await fs.FlushAsync();
		}
		var release = await Mediator.Send(new CreateReleaseAssemblyRequest(Id, filePath));
		return Ok(release);
	}

	[HttpGet("downloadRelease/{id}")]
	public async Task<IActionResult> DownloadLastRelease(Guid Id)
	{
		var release = await Mediator.Send(new GetLastAppsReleaseAssemblyRequest(Id));
		if (release == null)
			return NotFound();

		if (!System.IO.File.Exists(release.Path))
			return BadRequest();

		var provider = new FileExtensionContentTypeProvider();
		string contentType;
		if (!provider.TryGetContentType(release.Path, out contentType))
		{
			contentType ??= "application/octet-stream";
		}

		FileStream fileStream = new FileStream(release.Path, FileMode.Open, FileAccess.Read);
		return File(fileStream, contentType);
	}

	[HttpPut]
	public async Task<IActionResult> UpdateAppInfoAsync(UpdateAppRequest request)
	{
		var newApp = await Mediator.Send(request);
		if (newApp == null)
			return NotFound($"App with Id = {request.Id} not found!");

		return Ok();
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAppAsync(DeleteAppRequest app)
	{
		await Mediator.Send(app);
		return Ok();
	}
}
