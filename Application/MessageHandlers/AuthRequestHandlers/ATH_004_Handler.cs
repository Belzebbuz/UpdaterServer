using Application.DTO.AuthDTO.ATH_000;
using Application.DTO.AuthDTO.ATH_004;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace Application.MessageHandlers.AuthMessages;

public class ATH_004_Handler : IRequestHandler<ATH_004, IResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public ATH_004_Handler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IResponse> Handle(ATH_004 request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Contains("Admin"))
            return new ATH_400($"Нельзя удалить администратора, сначала удалите роль Admin у пользователя");
        await _userManager.DeleteAsync(user);
        return new ATH_000(200);
    }
}
