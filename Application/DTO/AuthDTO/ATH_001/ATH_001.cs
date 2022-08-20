namespace Application.DTO.AuthDTO.ATH_001;
/// <summary>
/// Сообщение для регистрации
/// </summary>
public class ATH_001 : IRequest<IResponse>
{
	public string Email { get; set; }
	public string Password { get; set; }
}
