using Application.Data;
using Application.DTO.LauncherDTO.LNC_000;
using Application.DTO.LauncherDTO.LNC_001;
using Application.DTO.LauncherDTO.LNC_002;
using Application.DTO.LauncherDTO.LNC_400;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Application.MessageHandlers.ProjectRequestHandlers;

public class LNC_001_Handler : IRequestHandler<LNC_001, IResponse>
{
	private readonly IRepository<Project> _repository;
	IHttpContextAccessor _httpContextAccessor;
	public LNC_001_Handler(IRepository<Project> repository, IHttpContextAccessor contextAccessor)
	{
		_repository = repository;
		_httpContextAccessor = contextAccessor;
	}
	public async Task<IResponse> Handle(LNC_001 request, CancellationToken cancellationToken)
	{
		var user = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
		var appExist = await _repository.AnyAsync(new FindProjByNameSpec(request.Name));
		if (appExist)
		{
			return new LNC_400("App already exists");
		}
		else
		{
			var newApp = new Project
			{
				Name = request.Name,
				Description = request.Description,
				ExeFile = request.ExeFile,
				IsWinService = request.IsWinService,
				UserEmail = user,
				Author = user,
				UpdateTime = DateTime.Now,
				CurrentVersion = "0.0.0.0"
			};
			await _repository.AddAsync(newApp);
			return new LNC_000(200);
		}
	}
}
