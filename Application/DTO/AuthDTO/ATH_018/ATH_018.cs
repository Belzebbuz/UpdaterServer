using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_018;

public class ATH_018 : IResponse
{
    public ATH_018(string userId, string userName, string passwordHash, List<AppUserRole> userRoles)
    {
        UserId = userId;
        UserName = userName;
        UserRoles = userRoles;
        PasswordHash = passwordHash;
    }

    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<AppUserRole> UserRoles { get; set; }
    public string PasswordHash { get; set; }
}
