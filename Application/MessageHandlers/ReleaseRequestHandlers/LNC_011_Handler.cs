using Application.DTO.LauncherDTO.LNC_009;
using Application.DTO.LauncherDTO.LNC_011;

namespace Application.MessageHandlers.ReleaseRequestHandlers;

public class LNC_011_Handler : IRequestHandler<LNC_011, IResponse>
{
	private readonly IReadRepository<ReleaseAssembly> _repository;

	public LNC_011_Handler(IReadRepository<ReleaseAssembly> repository)
	{
		_repository = repository;
	}
	public async Task<IResponse> Handle(LNC_011 request, CancellationToken cancellationToken)
	{
		return new LNC_009(await _repository.GetByIdAsync(request.Id));
	}
}
