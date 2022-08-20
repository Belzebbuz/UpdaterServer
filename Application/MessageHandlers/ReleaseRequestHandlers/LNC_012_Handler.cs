using Application.DTO.LauncherDTO.LNC_000;
using Application.DTO.LauncherDTO.LNC_012;
using Application.DTO.LauncherDTO.LNC_400;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.MessageHandlers.ReleaseRequestHandlers;

public class LNC_012_Handler : IRequestHandler<LNC_012, IResponse>
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IRepository<ReleaseAssembly> _repository;

	public LNC_012_Handler(IHttpContextAccessor httpContextAccessor, IRepository<ReleaseAssembly> repository)
	{
		_httpContextAccessor = httpContextAccessor;
		_repository = repository;
	}
	public async Task<IResponse> Handle(LNC_012 request, CancellationToken cancellationToken)
	{
		var release = await _repository.GetByIdAsync(request.ReleaseId);
		if (release == null)
			return new LNC_400("Release not found!");
		if (string.IsNullOrEmpty(request.Text))
			return new LNC_400("Text is empty!");
		var user = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
		release?.PatchNotes?.Add(new PatchNote() { Text = request.Text, UpdateTime = DateTime.Now, UserEmail = user });
		await _repository.UpdateAsync(release);
		return new LNC_000(200);
	}
}
