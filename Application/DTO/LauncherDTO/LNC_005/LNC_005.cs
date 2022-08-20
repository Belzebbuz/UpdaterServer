namespace Application.DTO.LauncherDTO.LNC_005;

public class LNC_005 : IRequest<IResponse>
{
	public bool? IsWinService { get; set; }
	public string? Name { get; set; }
}
