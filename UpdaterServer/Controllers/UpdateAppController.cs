using Microsoft.AspNetCore.Mvc;

namespace UpdaterServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UpdateAppController : ControllerBase
	{
		public UpdateAppController()
		{
		}

		[HttpGet("{appName}")]
		public async Task<string> CheckUpdateAsync(string appName)
		{
			return appName;
		}
	}
}
