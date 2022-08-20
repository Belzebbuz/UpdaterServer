namespace Application.DTO.AuthDTO.ATH_007;

public class ATH_007 : IResponse
{
	public ATH_007(List<ATH_007_User> users)
	{
		Users = users;
	}

	public List<ATH_007_User> Users { get;}
}
