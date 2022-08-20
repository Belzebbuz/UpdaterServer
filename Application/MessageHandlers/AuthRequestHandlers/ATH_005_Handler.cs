using Application.DTO.AuthDTO.ATH_000;
using Application.DTO.AuthDTO.ATH_005;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthMessages;

public class ATH_005_Handler : IRequestHandler<ATH_005, IResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public ATH_005_Handler(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<IResponse> Handle(ATH_005 request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        var roles = await _userManager.GetRolesAsync(user);
        if (user.Email == _configuration["SuperAdmin:Email"] && !request.SelectedRoles.Where(x => x.Value == "Admin").First().Check)
            return new ATH_400($"Невозможно лишить прав супер-администратора");

        var rolesToRemove = request.SelectedRoles.Where(x => !x.Check).Select(x => x.Value);
        var rolesToAdd = request.SelectedRoles.Where(x => x.Check).Select(x => x.Value);

        foreach (var role in rolesToRemove)
        {
            if (roles.Contains(role))
                await _userManager.RemoveFromRoleAsync(user, role);
        }
        foreach (var role in rolesToAdd)
        {
            if (!roles.Contains(role))
                await _userManager.AddToRoleAsync(user, role);
        }

        return new ATH_000(200);
    }
}
