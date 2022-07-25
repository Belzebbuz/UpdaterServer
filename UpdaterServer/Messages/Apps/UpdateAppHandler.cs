namespace UpdaterServer.Messages.Apps;

public class UpdateAppRequest : IRequest<Project?>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public bool IsWinService { get; set; }
	public string ExeFile { get; set; }
	public string Description { get; set; }
}
public class UpdateAppHandler : IRequestHandler<UpdateAppRequest, Project?>
{
	private readonly AppDbContext _appDbContext;

	public UpdateAppHandler(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<Project?> Handle(UpdateAppRequest request, CancellationToken cancellationToken)
	{
		var app = await _appDbContext.Projects.FindAsync(request.Id);
		if (app == null)
			return null;

		app.Name = request.Name;
		app.IsWinService = request.IsWinService;
		app.Description = request.Description;
		app.ExeFile = request.ExeFile;
		_appDbContext.Projects.Update(app);
		await _appDbContext.SaveChangesAsync();
		return app;
	}
}
