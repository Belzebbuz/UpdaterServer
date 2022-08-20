namespace Application.DTO.LauncherDTO.LNC_006;
/// <summary>
/// Запрос на обновление основной информации о приложении
/// </summary>
public class LNC_006 : IRequest<IResponse>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public bool IsWinService { get; set; }
	public string ExeFile { get; set; }
	public string Description { get; set; }
}
