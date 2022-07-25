using Microsoft.EntityFrameworkCore;

namespace UpdaterServer.Messages.Apps;

public class GetAppsByFilterRequest : IRequest<IEnumerable<Project>>
{
	public bool IsWinService { get; set; }
	public string Name { get; set; }
}
public class GetAppsByFilterHandler : IRequestHandler<GetAppsByFilterRequest, IEnumerable<Project>>
{
	private readonly AppDbContext _appDbContext;

	public GetAppsByFilterHandler(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}
	public async Task<IEnumerable<Project>> Handle(GetAppsByFilterRequest request, CancellationToken cancellationToken)
	{
		var apps = await _appDbContext.Projects.Where(x => x.IsWinService == request.IsWinService).ToListAsync();
		if (string.IsNullOrEmpty(request.Name))
			return apps;
		return apps
			.Where(x => x.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
	}
}
