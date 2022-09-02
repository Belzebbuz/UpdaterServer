namespace Application.DTO.AuthDTO.ATH_007;

public class ATH_007_User
{
	public ATH_007_User(string id, string email)
	{
		Id = id;
		Email = email;
	}

	public string Id { get; }
	public string Email { get; }
}