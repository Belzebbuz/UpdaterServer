namespace Application.DTO.AuthDTO.ATH_012;
/// <summary>
/// Запрос на добавление права в роль
/// </summary>
public class ATH_012 : IRequest<IResponse>
{
	public Guid UserRightId { get; set; }
	public string UserRoleId { get; set; }
}
