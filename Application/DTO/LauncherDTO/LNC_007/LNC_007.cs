namespace Application.DTO.LauncherDTO.LNC_007;
/// <summary>
/// Запрос на удаление приложения
/// </summary>
public class LNC_007 : IRequest<IResponse>
{
	public Guid Id { get; }
	public LNC_007(Guid id)
	{
		Id = id;
	}
}
