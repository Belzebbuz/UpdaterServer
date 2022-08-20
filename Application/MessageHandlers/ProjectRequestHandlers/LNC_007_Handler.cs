using Application.DTO.LauncherDTO.LNC_000;
using Application.DTO.LauncherDTO.LNC_007;
using Application.DTO.LauncherDTO.LNC_400;

namespace Application.MessageHandlers.ProjectRequestHandlers;

public class LNC_007_Handler : IRequestHandler<LNC_007, IResponse>
{
	private readonly IRepository<Project> _repository;

	public LNC_007_Handler(IRepository<Project> repository)
	{
		_repository = repository;
	}
	public async Task<IResponse> Handle(LNC_007 request, CancellationToken cancellationToken)
	{
		var app = await _repository.GetByIdAsync(request.Id);
		if (app == null)
			return new LNC_400("Project not found!");

		foreach (var path in app.ReleaseAssemblies.Where(x => File.Exists(x.Path)).Select(x => x.Path))
		{
			File.Delete(path);
		}
		//_appDbContext.ReleaseAssemblies.RemoveRange(app.ReleaseAssemblies);
		await _repository.DeleteAsync(app);
		return new LNC_000(200);
	}
}
