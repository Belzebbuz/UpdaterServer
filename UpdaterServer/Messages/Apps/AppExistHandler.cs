using Microsoft.EntityFrameworkCore;

namespace UpdaterServer.Messages.Apps;

public class AppExistRequest : IRequest<Project>
{
	public Guid AppId { get; set; }

	public AppExistRequest(Guid appId)
	{
		AppId = appId;
	}
}
public class AppExistHandler : IRequestHandler<AppExistRequest, Project>
{
	private readonly IServiceProvider _serviceProvider;

	public AppExistHandler(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}
	public async Task<Project> Handle(AppExistRequest request, CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();
		using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		return await context.Projects.FirstOrDefaultAsync(x => x.Id == request.AppId);
	}
}
