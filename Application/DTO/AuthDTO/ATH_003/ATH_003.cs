namespace Application.DTO.AuthDTO.ATH_003;
/// <summary>
/// Сообщение с попыткой авторизации
/// </summary>
public class ATH_003 :  IRequest<IResponse>
{
	public string Email { get; set; }
	public string Password { get; set; }
}
