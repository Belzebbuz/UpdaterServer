namespace Application.DTO.AuthDTO.ATH_020;

public class ATH_020_UserRole
{
	public ATH_020_UserRole(string id, string name, List<ATH_020_UserGroup> userGroups, List<ATH_020_UserRight> userRights)
	{
		Id = id;
		UserGroups = userGroups;
		UserRights = userRights;
		Name = name;
	}

	public string Id { get; }
	public string Name { get; }
	public List<ATH_020_UserGroup> UserGroups { get; }
	public List<ATH_020_UserRight> UserRights { get; }
}
