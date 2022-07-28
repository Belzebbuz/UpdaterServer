namespace UpdaterServer.Messages.Apps;

public class DeleteAppRequest : IRequest<bool>
{
	public Guid Id { get; set; }

	public DeleteAppRequest(Guid id)
	{
		Id = id;
	}
}
public class DeleteAppHandler : IRequestHandler<DeleteAppRequest,bool>
{
	private readonly AppDbContext _appDbContext;

	public DeleteAppHandler(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}
	public async Task<bool> Handle(DeleteAppRequest request, CancellationToken cancellationToken)
	{
		var app = await _appDbContext.Projects.FindAsync(request.Id);
		if (app == null)
			return false;
		foreach (var path in app.ReleaseAssemblies.Where(x => File.Exists(x.Path)).Select(x => x.Path))
		{
			File.Delete(path);
		}
		_appDbContext.ReleaseAssemblies.RemoveRange(app.ReleaseAssemblies);
		_appDbContext.Projects.Remove(app);
		await _appDbContext.SaveChangesAsync();
		return true;
	}
}
