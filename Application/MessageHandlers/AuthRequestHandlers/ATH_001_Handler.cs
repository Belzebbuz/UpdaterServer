using Application.DTO.AuthDTO.ATH_001;
using Application.DTO.AuthDTO.ATH_002;
using Application.DTO.AuthDTO.ATH_400;
using Application.Services.Common;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthMessages;

public class ATH_001_Handler : IRequestHandler<ATH_001, IResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;
    private readonly IReadRepository<AppUser> _readRepository;

    public ATH_001_Handler(UserManager<AppUser> userManager, IConfiguration configuration, IEmailSender emailSender, IReadRepository<AppUser> readRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailSender = emailSender;
        _readRepository = readRepository;
    }
    public async Task<IResponse> Handle(ATH_001 request, CancellationToken cancellationToken)
    {
        var user = new AppUser { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            string confirmCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmLink = Url.Action("ConfirmEmail", "Accounts", new { id = user.Id, code = confirmCode }, protocol: "http", host: _configuration["HostIP"]);
            //_emailSender.Send(user.Email, $"Перейдите по ссылке для подтверждения аккаунта <a href='{confirmLink}'>ссылка</a>", "Email confirmation");
            var creds = await _userManager.BuildTokenAsync(_readRepository, _configuration, request.Email);
            return new ATH_002(creds.Token, creds.Expiration);
        }
        else
        {
            return new ATH_400(result.Errors, "Registration error");
        }
    }
}
