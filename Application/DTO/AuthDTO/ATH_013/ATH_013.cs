namespace Application.DTO.AuthDTO.ATH_013;
/// <summary>
/// Запрос на добавление роли
/// </summary>
public class ATH_013 : IRequest<IResponse>
{
	public string Name { get; set; }
}
