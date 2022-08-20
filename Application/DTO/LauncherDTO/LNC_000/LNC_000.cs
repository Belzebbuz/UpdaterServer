namespace Application.DTO.LauncherDTO.LNC_000;

public class LNC_000 : IResponse
{
	public int Status { get; }

	public LNC_000(int status)
	{
		Status = status;
	}
}
