namespace Application.DTO.AuthDTO.ATH_007;

public class ATH_007_User
{
	public ATH_007_User(string id, string email, IList<string> roles)
	{
		Id = id;
		Email = email;
		Roles = roles;
	}

	public string Id { get; }
	public string Email { get; }
	public IList<string> Roles { get; }
}