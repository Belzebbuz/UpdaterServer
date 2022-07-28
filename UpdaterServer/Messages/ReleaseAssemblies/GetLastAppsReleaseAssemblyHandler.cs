namespace UpdaterServer.Messages.ReleaseAssemblies
{
	public class GetLastAppsReleaseAssemblyRequest : IRequest<ReleaseAssembly?>
	{
		public Guid Id { get; set; }
		public GetLastAppsReleaseAssemblyRequest(Guid id)
		{
			Id = id;
		}
	}
	public class GetLastAppsReleaseAssemblyHandler : IRequestHandler<GetLastAppsReleaseAssemblyRequest, ReleaseAssembly?>
	{
		private readonly IServiceProvider _serviceProvider;

		public GetLastAppsReleaseAssemblyHandler(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task<ReleaseAssembly?> Handle(GetLastAppsReleaseAssemblyRequest request, CancellationToken cancellationToken)
		{
			using var scope = _serviceProvider.CreateScope();
			using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			var app = await context.Projects.FindAsync(request.Id);
			if (app == null || !app.ReleaseAssemblies.Any())
				return null;
			return app.ReleaseAssemblies.Where(x => x.Version == app.CurrentVersion).OrderByDescending(x => x.ReleaseDate).First();
		}
	}
}
