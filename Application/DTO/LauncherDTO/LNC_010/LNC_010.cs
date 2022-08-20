namespace Application.DTO.LauncherDTO.LNC_010;
/// <summary>
/// Запрос на поиск приложения
/// </summary>
public class LNC_010 : IRequest<IResponse>
{
	public Guid Id { get; }

	public LNC_010(Guid id)
	{
		Id = id;
	}
}
