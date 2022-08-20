
using Application.DTO.LauncherDTO.LNC_003;
using Application.DTO.LauncherDTO.LNC_005;

namespace UpdaterServer.Messages.Apps;


public class LNC_005_Handler : IRequestHandler<LNC_005, IResponse>
{
	private readonly IReadRepository<Project> _repository;

	public LNC_005_Handler(IReadRepository<Project> repository)
	{
		_repository = repository;
	}
	public async Task<IResponse> Handle(LNC_005 request, CancellationToken cancellationToken)
	{
		if(request.IsWinService.HasValue)
		{
			var apps = await _repository.ListAsync(new FindByIsServiceSpec((bool)request.IsWinService));
			if (string.IsNullOrEmpty(request.Name))
				return new LNC_003(apps);

			return new LNC_003(apps.Where(x => x.Name.Contains(request.Name ?? String.Empty, StringComparison.OrdinalIgnoreCase)).ToList());
		}
		else
		{
			var apps = await _repository.ListAsync();
			return new LNC_003(apps);
		}
	}
}
