namespace Application.DTO.AuthDTO.ATH_002;
/// <summary>
/// Ответ на ATH_001, ATH_003
/// </summary>
public class ATH_002 : IResponse
{
	public ATH_002(string token, DateTime expiration)
	{
		Token = token;
		Expiration = expiration;
	}

	public string Token { get; }
	public DateTime Expiration { get; }
}
