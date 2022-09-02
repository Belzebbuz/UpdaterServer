using Application.DTO.AuthDTO.ATH_007;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthMessages;

public class GetAllUsersRequest : IRequest<IResponse> { }
public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, IResponse>
{
    private readonly IReadRepository<AppUser> _readRepository;
    private readonly UserManager<AppUser> _userManager;

    public GetAllUsersHandler(IReadRepository<AppUser> readRepository, UserManager<AppUser> userManager)
    {
        _readRepository = readRepository;
        _userManager = userManager;
    }
    public async Task<IResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _readRepository.ListAsync();
        var usersDTO = users.Select(x => new ATH_007_User(x.Id, x.Email)).ToList();
        return new ATH_007(usersDTO);
    }
}
