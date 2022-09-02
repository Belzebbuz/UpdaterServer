using Application.DTO.Common;
using Application.DTO.LauncherDTO.LNC_001;
using Application.DTO.LauncherDTO.LNC_005;
using Application.DTO.LauncherDTO.LNC_006;
using Application.DTO.LauncherDTO.LNC_007;
using Application.DTO.LauncherDTO.LNC_010;
using Application.MessageHandlers.ProjectRequestHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace UpdaterServer.Controllers;

[Route("api/[controller]")]
public class AppsController : BaseApiController
{
	[HttpGet]
	public async Task<IResponse> GetAllAppsAsync() => await Mediator.Send(new GetAllProjectsRequest());

	[HttpGet("{id}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IResponse> GetProjectAsync(Guid id) => await Mediator.Send(new LNC_010(id));

	[HttpPost("filter")]
	public async Task<IResponse> GetAppsByWinIsWinService(LNC_005 request) => await Mediator.Send(request);

	[HttpPost]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IResponse> CreateAppAsync(LNC_001 request) => await Mediator.Send(request);

	[HttpPut]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IResponse> UpdateAppInfoAsync(LNC_006 request) => await Mediator.Send(request);

	[HttpDelete("{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IResponse> DeleteAppAsync(Guid id) => await Mediator.Send(new LNC_007(id));

}
