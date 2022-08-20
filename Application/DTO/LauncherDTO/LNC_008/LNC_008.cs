namespace Application.DTO.LauncherDTO.LNC_008;
/// <summary>
/// Запрос на поиск релиза
/// </summary>
public class LNC_008 : IRequest<IResponse>
{
	public Guid Id { get; set; }

	public LNC_008(Guid id)
	{
		Id = id;
	}
}
