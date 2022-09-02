namespace Application.DTO.AuthDTO.ATH_020;

public class ATH_020_UserRight
{
	public ATH_020_UserRight(Guid id, string name)
	{
		Id = id;
		Name = name;
	}

	public Guid Id { get; }
	public string Name { get; }
}