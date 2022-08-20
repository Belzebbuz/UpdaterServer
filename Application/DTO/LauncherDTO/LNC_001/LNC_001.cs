namespace Application.DTO.LauncherDTO.LNC_001;
/// <summary>
/// Запрос на создание нового приложения
/// </summary>
public class LNC_001 : IRequest<IResponse>
{
	public string Name { get; set; }
	public bool IsWinService { get; set; }
	public string ExeFile { get; set; }
	public string Description { get; set; }
}
