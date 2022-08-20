using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_016;

public class ATH_016 : IResponse
{
	public ATH_016(string id, string name, List<UserGroup> userGroups, List<UserRight> userRights)
	{
		Id = id;
		Name = name;
		UserGroups = userGroups;
		UserRights = userRights;
	}

	public string Id { get; }
	public string Name { get; }
	public List<UserGroup> UserGroups { get; }
	public List<UserRight> UserRights { get; }
}
