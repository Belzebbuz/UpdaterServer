
using Application.Data;
using Application.DTO.LauncherDTO.LNC_003;

namespace Application.MessageHandlers.ProjectRequestHandlers;

public class GetAllProjectsRequest : IRequest<IResponse> { }
public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsRequest, IResponse>
{
	private readonly IReadRepository<Project> _repository;

	public GetAllProjectsHandler(IReadRepository<Project> repository)
	{
		_repository = repository;
	}
	public async Task<IResponse> Handle(GetAllProjectsRequest request, CancellationToken cancellationToken)
	{
		return new LNC_003(await _repository.ListAsync());
	}
}
