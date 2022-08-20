using Application.DTO.AuthDTO.ATH_002;
using Application.DTO.AuthDTO.ATH_400;
using Application.Extensions;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.MessageHandlers.AuthMessages;

public class UpdateTokenRequest : IRequest<IResponse> { }
public class UpdateTokenHandler : IRequestHandler<UpdateTokenRequest, IResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReadRepository<AppUser> _readRepository;

    public UpdateTokenHandler(UserManager<AppUser> userManager, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor, IReadRepository<AppUser> readRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _readRepository = readRepository;
    }
    public async Task<IResponse> Handle(UpdateTokenRequest request, CancellationToken cancellationToken)
    {
        var userEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        if (userEmail == null)
            return new ATH_400("Update token error");

        var creds = await _userManager.BuildTokenAsync(_readRepository,_configuration, userEmail);
        return new ATH_002(creds.Token, creds.Expiration);
    }
}
