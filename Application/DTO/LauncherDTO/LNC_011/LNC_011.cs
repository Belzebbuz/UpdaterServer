namespace Application.DTO.LauncherDTO.LNC_011;
/// <summary>
/// Запрос на получение последней актуальной сборки.
/// Ответ LNC_009 или null
/// </summary>
public class LNC_011 : IRequest<IResponse>
{
	public Guid Id { get; }

	public LNC_011(Guid id)
	{
		Id = id;
	}
}
