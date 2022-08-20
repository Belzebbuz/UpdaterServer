using Application.DTO.AuthDTO.ATH_000;
using Application.DTO.AuthDTO.ATH_013;
using Application.DTO.AuthDTO.ATH_015;
using Application.DTO.AuthDTO.ATH_016;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthRequestHandlers;

public class ATH_015_Handler : IRequestHandler<ATH_015, IResponse>
{
	private readonly IRepository<AppUserRole> _roleRepository;
	private readonly IRepository<UserGroup> _userGroupRepository;

	public ATH_015_Handler(IRepository<AppUserRole> roleRepository, IRepository<UserGroup> userGroupRepository)
	{
		_roleRepository = roleRepository;
		_userGroupRepository = userGroupRepository;
	}
	public async Task<IResponse> Handle(ATH_015 request, CancellationToken cancellationToken)
	{
		var role = await _roleRepository.FirstOrDefaultAsync(new ATH_015_Spec.FindRoleByIdSpec(request.RoleId));
		if (role == null)
			return new ATH_400("Role not found");

		var userGroup = await _userGroupRepository.GetByIdAsync(request.UserGroupId);

		if (userGroup == null)
			return new ATH_400("User not found");

		if (role.UserGroups.Any(x => x.Id == userGroup.Id))
			return new ATH_400("Group already exists!");

		role.UserGroups.Add(userGroup);
		await _roleRepository.UpdateAsync(role);
		return new ATH_016(role.Id, role.Name, role.UserGroups, role.UserRights);
	}
}
