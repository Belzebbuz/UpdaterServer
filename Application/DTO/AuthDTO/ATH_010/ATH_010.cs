namespace Application.DTO.AuthDTO.ATH_010;
/// <summary>
/// Запрос на добавление пользователя в группу пользователей
/// </summary>
public class ATH_010 : IRequest<IResponse>
{
	public string UserId { get; set; }
	public Guid UserGroupId { get; set; }
}
