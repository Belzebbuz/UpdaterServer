using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdaterServer.Domain;
using UpdaterServer.Domain.Enties;

namespace UpdaterServer.Messages.ReleaseAssemblies
{
	public class CreateReleaseAssemblyRequest : IRequest<ReleaseAssembly>
	{
		public Guid AppId { get; set; }
		public string Path { get; set; } = default!;

		public CreateReleaseAssemblyRequest(Guid appId, string path)
		{
			AppId = appId;
			Path = path;
		}
	}
	public class CreateReleaseHandler : IRequestHandler<CreateReleaseAssemblyRequest, ReleaseAssembly>
	{
		private readonly IServiceProvider _serviceProvider;

		public CreateReleaseHandler(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		public async Task<ReleaseAssembly> Handle(CreateReleaseAssemblyRequest request, CancellationToken cancellationToken)
		{
			using var scope = _serviceProvider.CreateScope();
			using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			var app = await context.Projects.FindAsync(request.AppId);
			if (app == null)
				return null;
			var release = new ReleaseAssembly { Path = request.Path, ReleaseDate = DateTime.Now };
			app?.ReleaseAssemblies?.Add(release);
			await context.SaveChangesAsync();
			return release;
		}
	}
}
