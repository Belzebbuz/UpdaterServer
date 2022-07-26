using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace UpdaterServer.Messages.Apps;

public class CreateAppRequest : IRequest<Guid?>
{
	public string Name { get; set; }
	public bool IsWinService { get; set; }
	public string ExeFile { get; set; }
	public string Description { get; set; }
}
public class CreateAppHandler : IRequestHandler<CreateAppRequest, Guid?>
{
	private readonly AppDbContext _appDbContext;
	IHttpContextAccessor _httpContextAccessor;
	public CreateAppHandler(AppDbContext appDbContext, IHttpContextAccessor contextAccessor)
	{
		_appDbContext = appDbContext;
		_httpContextAccessor = contextAccessor;
	}
	public async Task<Guid?> Handle(CreateAppRequest request, CancellationToken cancellationToken)
	{
		var user = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
		var appExist = await _appDbContext.Projects.AnyAsync(x => request.Name == x.Name);
		if (appExist)
		{
			return null;
		}
		else
		{
			var newApp = new Project
			{
				Name = request.Name,
				Description = request.Description,
				ExeFile = request.ExeFile,
				IsWinService = request.IsWinService,
				UserEmail = user,
				Author = user,
				UpdateTime = DateTime.Now
			};
			await _appDbContext.AddAsync(newApp);
			await _appDbContext.SaveChangesAsync();
			return newApp.Id;
		}
	}
}
