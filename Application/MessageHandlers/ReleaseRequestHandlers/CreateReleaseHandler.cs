using Application.DTO.LauncherDTO.LNC_009;
using Application.DTO.LauncherDTO.LNC_400;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.MessageHandlers.ReleaseRequestHandlers;

public class CreateReleaseAssemblyRequest : IRequest<IResponse>
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
public class CreateReleaseHandler : IRequestHandler<CreateReleaseAssemblyRequest, IResponse>
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IRepository<Project> _repository;

	public CreateReleaseHandler(IHttpContextAccessor httpContextAccessor, IRepository<Project> repository)
	{
		_httpContextAccessor = httpContextAccessor;
		_repository = repository;
	}
	public async Task<IResponse> Handle(CreateReleaseAssemblyRequest request, CancellationToken cancellationToken)
	{
		var user = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
		var app = await _repository.GetByIdAsync(request.AppId);
		if (app == null)
			return new LNC_400("Project not found!");

		var release = new ReleaseAssembly
		{
			Path = request.Path,
			UpdateTime = DateTime.Now,
			UserEmail = user,
			Version = request.Version,
			PatchNotes = new()
		};

		app.ReleaseAssemblies.Add(release);
		if (IsCurrentVersionLower(app.CurrentVersion, request.Version))
			app.CurrentVersion = request.Version;
		await _repository.UpdateAsync(app);
		return new LNC_009(release);
	}

	private bool IsCurrentVersionLower(string currentVersion, string newVersion)
	{
		if (string.IsNullOrEmpty(currentVersion))
			return true;
		if (int.TryParse(currentVersion.Replace(".", ""), out int currentVersionParsed)
			&& int.TryParse(newVersion.Replace(".", ""), out int newVersionParsed))
		{
			return currentVersionParsed < newVersionParsed;
		}
		return false;
	}
}