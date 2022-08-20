using Application.DTO.AuthDTO.ATH_000;
using Application.DTO.AuthDTO.ATH_012;
using Application.DTO.AuthDTO.ATH_014;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthRequestHandlers;

public class ATH_012_Handler : IRequestHandler<ATH_012, IResponse>
{
	private readonly IRepository<AppUserRole> _roleRepository;
	private readonly IRepository<UserRight> _rightRepository;

	public ATH_012_Handler(IRepository<AppUserRole> roleRepository, IRepository<UserRight> rightRepository)
	{
		_roleRepository = roleRepository;
		_rightRepository = rightRepository;
	}
	public async Task<IResponse> Handle(ATH_012 request, CancellationToken cancellationToken)
	{
		var role = await _roleRepository.FirstOrDefaultAsync(new ATH_012_Spec.FindRoleByIdSpec(request.UserRoleId));
		if (role == null)
			return new ATH_400("Role not found!");
		var right = await _rightRepository.GetByIdAsync(request.UserRightId);
		if (right == null)
			return new ATH_400("Right not found");
		if (role.UserRights.Any(x => x.Id == right.Id))
			return new ATH_400("This right already exists in Role");

		role.UserRights.Add(right);
		await _roleRepository.UpdateAsync(role);
		return new ATH_014(role.Id, role.Name, role.UserRights);
	}
}
