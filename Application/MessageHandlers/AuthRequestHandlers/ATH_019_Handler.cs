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
		return new ATH_020(roles);
	}
}
