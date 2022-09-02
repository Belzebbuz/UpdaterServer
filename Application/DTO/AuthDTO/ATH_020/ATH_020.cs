using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_020;

public class ATH_020 : IResponse
{
	public ATH_020(List<ATH_020_UserRole> userRoles)
	{
		UserRoles = userRoles;
	}

	public List<ATH_020_UserRole> UserRoles { get; }
}
