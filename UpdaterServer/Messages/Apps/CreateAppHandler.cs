using Microsoft.EntityFrameworkCore;

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

	public CreateAppHandler(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}
	public async Task<Guid?> Handle(CreateAppRequest request, CancellationToken cancellationToken)
	{
		var appExist = await _appDbContext.Projects.AnyAsync(x => request.Name == x.Name);
		if (appExist)
		{
			return null;
		}
		else
		{
			var newApp = new Project { Name = request.Name, Description = request.Description, ExeFile = request.ExeFile, IsWinService = request.IsWinService };
			await _appDbContext.AddAsync(newApp);
			await _appDbContext.SaveChangesAsync();
			return newApp.Id;
		}
	}
}
