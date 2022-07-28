using Microsoft.AspNetCore.Mvc;

namespace UpdaterServer.Controllers;

[ApiController]
public class BaseApiController : ControllerBase
{
	private ISender _mediator = null!;

	protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
	protected string ReleasesPath 
	{ 
		get 
		{
			var configPath = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["ReleasesPath"];
			string path = configPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "releases");
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			return path;
		}
	}
}
