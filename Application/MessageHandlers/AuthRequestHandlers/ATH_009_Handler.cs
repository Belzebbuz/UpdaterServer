using Application.DTO.AuthDTO.ATH_000;
using Application.DTO.AuthDTO.ATH_009;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthRequestHandlers;

public class ATH_009_Handler : IRequestHandler<ATH_009, IResponse>
{
	private readonly IRepository<UserGroup> _repository;

	public ATH_009_Handler(IRepository<UserGroup> repository)
	{
		_repository = repository;
	}
	public async Task<IResponse> Handle(ATH_009 request, CancellationToken cancellationToken)
	{
		var exist = await _repository.AnyAsync(new ATH_009_Spec.FindUserGroupByNameSpec(request.Name));
		if (exist)
			return new ATH_400("User group already exists!");
		await _repository.AddAsync(new UserGroup() { Name = request.Name });
		return new ATH_000(200);
	}
}
