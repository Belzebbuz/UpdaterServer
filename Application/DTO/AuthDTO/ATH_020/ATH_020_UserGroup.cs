namespace Application.DTO.AuthDTO.ATH_020;

public class ATH_020_UserGroup
{
	public ATH_020_UserGroup(Guid id, string name)
	{
		Id = id;
		Name = name;
	}

	public Guid Id { get; }
	public string Name { get; }
}
