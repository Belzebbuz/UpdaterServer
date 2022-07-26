namespace UpdaterServer.DTOs;

public class RoleEditDTO
{
	public string UserId { get; set; }
	public List<SelectedRole> SelectedRoles { get; set;}
}
public class SelectedRole
{
	public string Value { get; set; }
	public bool Check { get; set; }
}

