using Application.DTO.LauncherDTO.LNC_008;
using Application.DTO.LauncherDTO.LNC_009;

namespace Application.MessageHandlers.ReleaseRequestHandlers;

public class LNC_008_Handler : IRequestHandler<LNC_008, IResponse>
{
	private readonly IReadRepository<ReleaseAssembly> _repository;

	public LNC_008_Handler(IReadRepository<ReleaseAssembly> repository)
	{
		_repository = repository;
	}
	public async Task<IResponse> Handle(LNC_008 request, CancellationToken cancellationToken)
	{
		return new LNC_009(await _repository.GetByIdAsync(request.Id));
	}
}
