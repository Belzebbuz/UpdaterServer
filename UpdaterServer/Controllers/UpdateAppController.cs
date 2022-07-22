using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpdaterServer.Domain;
using UpdaterServer.Domain.Enties;

namespace UpdaterServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UpdateAppController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;

		public UpdateAppController(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		[HttpGet]
		public async Task<List<ProjectAssembly>> GetAllLastReleasesAsync()
		{
			var result = await _appDbContext.ProjectAssemblies.ToListAsync();
			return result.DistinctBy(x => x.Name).ToList();
		}

		[HttpGet("{appName}")]
		public async Task<IActionResult> GetLastReleaseAsync(string appName)
		{
			var result =  await _appDbContext.ProjectAssemblies.FirstOrDefaultAsync(x => x.Name == appName);
			if (result == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(result);
			}
		}
	}
}
