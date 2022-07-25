using Microsoft.EntityFrameworkCore;

namespace UpdaterServer.Messages.Apps;

public class GetProjectByIdRequest : IRequest<Project>
{
	public Guid Id { get; set; }

	public GetProjectByIdRequest(Guid id)
	{
		Id = id;
	}
}

public class GetProjectByIdHander : IRequestHandler<GetProjectByIdRequest, Project>
{
	private readonly AppDbContext _appDbContext;

	public GetProjectByIdHander(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}
	public async Task<Project> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
	{
		return await _appDbContext.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);
	}
}