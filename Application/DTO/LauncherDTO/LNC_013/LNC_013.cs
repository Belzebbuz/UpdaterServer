namespace Application.DTO.LauncherDTO.LNC_013;
/// <summary>
/// Запрос на удаление патч ноута
/// </summary>
public class LNC_013 : IRequest<IResponse>
{
	public Guid Id { get; }

	public LNC_013(Guid id)
	{
		Id = id;
	}
}
