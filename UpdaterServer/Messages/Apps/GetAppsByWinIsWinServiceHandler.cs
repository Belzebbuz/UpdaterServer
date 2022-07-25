using Microsoft.EntityFrameworkCore;

namespace UpdaterServer.Messages.Apps;

public class GetAppsByWinIsWinServiceRequest : IRequest<List<Project>>
{
	public bool IsWinService { get; set; }
}
public class GetAppsByWinIsWinServiceHandler : IRequestHandler<GetAppsByWinIsWinServiceRequest, List<Project>>
{
	private readonly AppDbContext _appDbContext;

	public GetAppsByWinIsWinServiceHandler(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<List<Project>> Handle(GetAppsByWinIsWinServiceRequest request, CancellationToken cancellationToken)
	{
		return await _appDbContext.Projects.Where(x => x.IsWinService == request.IsWinService).ToListAsync();
	}
}
