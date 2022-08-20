namespace Application.DTO.AuthDTO.ATH_004;
/// <summary>
/// Запрос на удаление пользователя
/// </summary>
public class ATH_004 : IRequest<IResponse>
{
	public string Id { get;}
	public ATH_004(string id)
	{
		Id = id;
	}
}
