using Application.DTO.AuthDTO.ATH_000;
using Application.DTO.AuthDTO.ATH_008;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthRequestHandlers;

public class ATH_008_Handler : IRequestHandler<ATH_008, IResponse>
{
	private readonly IRepository<UserRight> _repository;

	public ATH_008_Handler(IRepository<UserRight> repository)
	{
		_repository = repository;
	}
	public async Task<IResponse> Handle(ATH_008 request, CancellationToken cancellationToken)
	{
		var exist = await _repository.AnyAsync(new ATH_009_Spec.FindUserRightByNameSpec(request.Name));
		if (exist)
			return new ATH_400("User right already exists!");

		await _repository.AddAsync(new UserRight() { Name = request.Name });

		return new ATH_000(200);
	}
}
