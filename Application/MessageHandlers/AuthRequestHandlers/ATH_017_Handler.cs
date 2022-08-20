using Application.DTO.AuthDTO.ATH_017;
using Application.DTO.AuthDTO.ATH_018;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthRequestHandlers;

public class ATH_017_Handler : IRequestHandler<ATH_017, IResponse>
{
	private readonly IReadRepository<AppUserRole> _readRepository;
	private readonly IReadRepository<AppUser> _userRepository;

	public ATH_017_Handler(IReadRepository<AppUserRole> readRepository, IReadRepository<AppUser> userRepository)
	{
		_readRepository = readRepository;
		_userRepository = userRepository;
	}
	public async Task<IResponse> Handle(ATH_017 request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByIdAsync(request.UserId);
		if(user == null)
			return new ATH_400("user not found");
		var roles = await _readRepository.ListAsync(new ATH_017_Spec.FindRolesByUserIdSpec(request.UserId));
		if (!roles.Any())
			return new ATH_400("roles not found");

		return new ATH_018(user.Id, user.UserName, user.PasswordHash, roles);
	}
}
