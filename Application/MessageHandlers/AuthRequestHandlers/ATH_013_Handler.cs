using Application.DTO.AuthDTO.ATH_000;
using Application.DTO.AuthDTO.ATH_013;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthRequestHandlers;

public class ATH_013_Handler : IRequestHandler<ATH_013, IResponse>
{
	private readonly RoleManager<AppUserRole> _roleManager;

	public ATH_013_Handler(RoleManager<AppUserRole> roleManager)
	{
		_roleManager = roleManager;
	}
	public async Task<IResponse> Handle(ATH_013 request, CancellationToken cancellationToken)
	{
		var role = await _roleManager.FindByNameAsync(request.Name);

		if (role != null)
			return new ATH_400("Role already exists!");

		await _roleManager.CreateAsync(new AppUserRole() { Name = request.Name });

		return new ATH_000(200);
	}
}
