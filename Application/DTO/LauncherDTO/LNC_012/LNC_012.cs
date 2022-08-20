namespace Application.DTO.LauncherDTO.LNC_012;
/// <summary>
/// Запрос на добавление патч ноутов
/// </summary>
public class LNC_012 : IRequest<IResponse>
{
	public Guid ReleaseId { get; set; }
	public string Text { get; set; }
}
