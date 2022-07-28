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
		public string Version { get; set; }
		public CreateReleaseAssemblyRequest(Guid appId, string path, string version)
		{
			AppId = appId;
			Path = path;
			Version = version;
		}
	}
	public class CreateReleaseHandler : IRequestHandler<CreateReleaseAssemblyRequest, ReleaseAssembly>
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CreateReleaseHandler(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
		{
			_serviceProvider = serviceProvider;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<ReleaseAssembly> Handle(CreateReleaseAssemblyRequest request, CancellationToken cancellationToken)
		{
			var user = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
			using var scope = _serviceProvider.CreateScope();
			using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			var app = await context.Projects.FindAsync(request.AppId);
			if (app == null)
				return null;
			var release = new ReleaseAssembly { Path = request.Path, UpdateTime = DateTime.Now, UserEmail = user, Version = request.Version };
			app.ReleaseAssemblies.Add(release);
			if(IsCurrentVersionLower(app.CurrentVersion, request.Version))
				app.CurrentVersion = request.Version;
			await context.SaveChangesAsync();
			return release;
		}

		private bool IsCurrentVersionLower(string currentVersion, string newVersion)
		{
			if (string.IsNullOrEmpty(currentVersion))
				return true;
			if(int.TryParse(currentVersion.Replace(".",""), out int currentVersionParsed) 
				&& int.TryParse(newVersion.Replace(".", ""), out int newVersionParsed))
			{
				return currentVersionParsed < newVersionParsed ? true : false;
			}
			return false;
		}
	}
}
