using Microsoft.EntityFrameworkCore;

namespace UpdaterServer.Messages.Apps;

public class AppExistRequest : IRequest<bool>
{
	public Guid AppId { get; set; }

	public AppExistRequest(Guid appId)
	{
		AppId = appId;
	}
}
public class AppExistHandler : IRequestHandler<AppExistRequest, bool>
{
	private readonly IServiceProvider _serviceProvider;

	public AppExistHandler(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}
	public async Task<bool> Handle(AppExistRequest request, CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();
		using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		return await context.Projects.AnyAsync(x => x.Id == request.AppId);
	}
}
