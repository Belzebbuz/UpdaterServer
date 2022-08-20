namespace Application.DTO.AuthDTO.ATH_008;
/// <summary>
/// Запрос на создание права доступа
/// </summary>
public class ATH_008 : IRequest<IResponse>
{
	public string Name { get; set; }
}
