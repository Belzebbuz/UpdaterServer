namespace UpdaterServer.Messages.Apps;

public class DeleteAppRequest : IRequest
{
	public Guid Id { get; set; }
}
public class DeleteAppHandler : IRequestHandler<DeleteAppRequest>
{
	private readonly AppDbContext _appDbContext;

	public DeleteAppHandler(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}
	public async Task<Unit> Handle(DeleteAppRequest request, CancellationToken cancellationToken)
	{
		var app = await _appDbContext.Projects.FindAsync(request.Id);
		if (app == null)
			return new Unit();
		foreach (var path in app.ReleaseAssemblies.Where(x => File.Exists(x.Path)).Select(x => x.Path))
		{
			File.Delete(path);
		}
		_appDbContext.Projects.Remove(app);
		await _appDbContext.SaveChangesAsync();
		return new Unit();
	}
}
