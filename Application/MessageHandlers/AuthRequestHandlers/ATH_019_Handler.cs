using Application.DTO.AuthDTO.ATH_019;
using Application.DTO.AuthDTO.ATH_020;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthRequestHandlers;

public class ATH_019_Handler : IRequestHandler<ATH_019, IResponse>
{
	private readonly IReadRepository<AppUserRole> _readRepository;

	public ATH_019_Handler(IReadRepository<AppUserRole> readRepository)
	{
		_readRepository = readRepository;
	}
	public async Task<IResponse> Handle(ATH_019 request, CancellationToken cancellationToken)
	{
		var roles = await _readRepository.ListAsync(new ATH_019_Spec.FindAllRolesSpec());
		List<ATH_020_UserRole> rolesDto = new();
		foreach (var role in roles)
		{
			List<ATH_020_UserGroup> userGroups = new();
			List<ATH_020_UserRight> userRights = new();
			role.UserGroups.ForEach(x => userGroups.Add(new(x.Id, x.Name)));
			role.UserRights.ForEach(x => userRights.Add(new(x.Id, x.Name)));
			ATH_020_UserRole roleDto = new(role.Id, role.Name, userGroups, userRights);
			rolesDto.Add(roleDto);
		}
		return new ATH_020(rolesDto);
	}
}
