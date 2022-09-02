using Application.DTO.LauncherDTO.LNC_000;
using Application.DTO.LauncherDTO.LNC_004;
using Application.DTO.LauncherDTO.LNC_006;
using Application.DTO.LauncherDTO.LNC_400;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.MessageHandlers.ProjectRequestHandlers;

public class LNC_006_Handler : IRequestHandler<LNC_006, IResponse>
{
	private readonly IRepository<Project> _repository;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public LNC_006_Handler(IRepository<Project> repository, IHttpContextAccessor httpContextAccessor)
	{
		_repository = repository;
		_httpContextAccessor = httpContextAccessor;
	}
	public async Task<IResponse> Handle(LNC_006 request, CancellationToken cancellationToken)
	{
		var user = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
		var app = await _repository.GetByIdAsync(request.Id);
		if (app == null)
			return new LNC_400("Project not found!");

		app.Name = request.Name;
		app.IsWinService = request.IsWinService;
		app.Description = request.Description;
		app.ExeFile = request.ExeFile;
		app.UserEmail = user;
		app.UpdateTime = DateTime.Now;
		await _repository.UpdateAsync(app);
		return new LNC_000(200);
	}
}
