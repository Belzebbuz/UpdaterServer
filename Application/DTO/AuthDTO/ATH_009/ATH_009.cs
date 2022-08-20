namespace Application.DTO.AuthDTO.ATH_009;
/// <summary>
/// Запрос на создание группы пользователей
/// </summary>
public class ATH_009 : IRequest<IResponse>
{
	public string Name { get; set; }
}
