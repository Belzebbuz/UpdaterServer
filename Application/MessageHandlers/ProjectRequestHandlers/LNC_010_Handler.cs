using Application.Data;
using Application.DTO.LauncherDTO.LNC_004;
using Application.DTO.LauncherDTO.LNC_010;

namespace Application.MessageHandlers.ProjectRequestHandlers;

public class LNC_010_Handler : IRequestHandler<LNC_010, IResponse>
{
	private readonly IReadRepository<Project> _repository;

	public LNC_010_Handler(IReadRepository<Project> repository)
	{
		_repository = repository;
	}
	public async Task<IResponse> Handle(LNC_010 request, CancellationToken cancellationToken)
	{
		return new LNC_004(await _repository.FirstOrDefaultAsync(new LNC_010_Spec.FindByIdProjectSpec(request.Id)));
	}
}