using Application.DTO.AuthDTO.ATH_001;
using Application.DTO.AuthDTO.ATH_003;
using Application.DTO.AuthDTO.ATH_004;
using Application.DTO.AuthDTO.ATH_005;
using Application.DTO.AuthDTO.ATH_008;
using Application.DTO.AuthDTO.ATH_009;
using Application.DTO.AuthDTO.ATH_010;
using Application.DTO.AuthDTO.ATH_012;
using Application.DTO.AuthDTO.ATH_013;
using Application.DTO.AuthDTO.ATH_015;
using Application.DTO.AuthDTO.ATH_017;
using Application.DTO.AuthDTO.ATH_019;
using Application.DTO.Common;
using Application.MessageHandlers.AuthMessages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpdaterServer.AuthAttributes;

namespace UpdaterServer.Controllers;

[Route("[controller]")]
public class AccountsController : ControllerBase
{
	private readonly ISender _mediatr;

	public AccountsController(ISender mediatr)
	{
		_mediatr = mediatr;
	}

	[HttpGet("listUsers")]
	[ClaimRequirement(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Right = "Use trace x 3 line")]
	public async Task<IResponse> GetListUsers() => await _mediatr.Send(new GetAllUsersRequest());

	[HttpPost("create")]
	public async Task<IResponse> Create([FromBody] ATH_001 request) => await _mediatr.Send(request);
	
	[HttpPost("login")]
	public async Task<IResponse> Login([FromBody] ATH_003 request) => await _mediatr.Send(request);

	[HttpGet("delete/{id}")]
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<IResponse> DeleteUser(string id) => await _mediatr.Send(new ATH_004(id));

	[HttpPost("editRoles")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<IResponse> EditRoles([FromBody] ATH_005 request) => await _mediatr.Send(request);

	[HttpGet("getjwt")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<IResponse> GetJwt() => await _mediatr.Send(new UpdateTokenRequest());

	[HttpGet]
	public async Task<string> ConfirmEmail(string id, string code) => await _mediatr.Send(new ConfirmEmailRequest(id, code));

	[HttpPost("addUserGroup")]
	public async Task<IResponse> AddUserGroup([FromBody] ATH_009 request) => await _mediatr.Send(request);

	[HttpPost("addUserToUserGroup")]
	public async Task<IResponse> AddUserToUserGroup([FromBody] ATH_010 request) => await _mediatr.Send(request);

	[HttpPost("addUserRight")]
	public async Task<IResponse> AddUserRight([FromBody] ATH_008 request) => await _mediatr.Send(request);

	[HttpPost("addUserRightToUserRole")]
	public async Task<IResponse> AddUserRight([FromBody] ATH_012 request) => await _mediatr.Send(request);

	[HttpPost("addUserRole")]
	public async Task<IResponse> AddUserRole([FromBody] ATH_013 request) => await _mediatr.Send(request);
	[HttpPost("addUserGroupToUserRole")]
	public async Task<IResponse> AddUserGroupToUserRole([FromBody] ATH_015 request) => await _mediatr.Send(request);

	[HttpGet("getUserGroups/{id}")]
	public async Task<IResponse> AddUserGroupToUserRole(string id) => await _mediatr.Send(new ATH_017 { UserId = id });
	[HttpGet("roles")]
	public async Task<IResponse> AddUserGroupToUserRole() => await _mediatr.Send(new ATH_019());

}
