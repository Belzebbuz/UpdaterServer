namespace Application.DTO.LauncherDTO.LNC_009;

public class LNC_009 : IResponse
{
	public ReleaseAssembly? Release { get; }

	public LNC_009(ReleaseAssembly? release)
	{
		Release = release;
	}
}
