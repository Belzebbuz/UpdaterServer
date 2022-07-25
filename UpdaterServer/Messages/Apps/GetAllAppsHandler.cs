using Microsoft.EntityFrameworkCore;

namespace UpdaterServer.Messages.Apps;

public class GetAllAppsRequest : IRequest<List<Project>> { }
public class GetAllAppsHandler : IRequestHandler<GetAllAppsRequest, List<Project>>
{
	private readonly AppDbContext _appDbContext;

	public GetAllAppsHandler(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}
	public async Task<List<Project>> Handle(GetAllAppsRequest request, CancellationToken cancellationToken)
	{
		return await _appDbContext.Projects.ToListAsync();
	}
}
