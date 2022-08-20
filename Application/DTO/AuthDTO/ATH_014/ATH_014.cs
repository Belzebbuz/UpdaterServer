using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_014;

public class ATH_014 : IResponse
{
	public ATH_014(string id, string name, List<UserRight> userRights)
	{
		Id = id;
		Name = name;
		UserRights = userRights;
	}

	public string Id { get; }
	public string Name { get; }
	public List<UserRight> UserRights{ get; }
}
