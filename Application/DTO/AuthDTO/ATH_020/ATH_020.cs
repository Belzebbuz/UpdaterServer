using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_020;

public class ATH_020 : IResponse
{
	public ATH_020(List<AppUserRole> userRoles)
	{
		UserRoles = userRoles;
	}

	public List<AppUserRole> UserRoles { get; }
}
