using Application.DTO.LauncherDTO.LNC_000;
using Application.DTO.LauncherDTO.LNC_013;
using Application.DTO.LauncherDTO.LNC_400;

namespace Application.MessageHandlers.ReleaseRequestHandlers;

public class LNC_013_Handler : IRequestHandler<LNC_013, IResponse>
{
	private readonly IRepository<PatchNote> _repository;

	public LNC_013_Handler(IRepository<PatchNote> repository)
	{
		_repository = repository;
	}
	public async Task<IResponse> Handle(LNC_013 request, CancellationToken cancellationToken)
	{
		var note = await _repository.GetByIdAsync(request.Id);
		if (note == null)
			return new LNC_400("Patch note not found!");
		await _repository.DeleteAsync(note);
		return new LNC_000(200);
	}
}
