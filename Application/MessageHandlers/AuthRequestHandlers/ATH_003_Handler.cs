using Application.DTO.AuthDTO.ATH_002;
using Application.DTO.AuthDTO.ATH_003;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthMessages;

public class ATH_003_Handler : IRequestHandler<ATH_003, IResponse>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IReadRepository<AppUser> _readRepository;

    public ATH_003_Handler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration, IReadRepository<AppUser> readRepository)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _readRepository = readRepository;
    }
    public async Task<IResponse> Handle(ATH_003 request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email,
        request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var creds = await _userManager.BuildTokenAsync(_readRepository, _configuration, request.Email);
            return new ATH_002(creds.Token, creds.Expiration);
            //if (user.EmailConfirmed)
            //	return await BuildToken(request.Email);
            //else
            //	return BadRequest("Email is not confirmed");
        }
        else
        {
            return new ATH_400("Invalid login or password");
        }
    }
}
