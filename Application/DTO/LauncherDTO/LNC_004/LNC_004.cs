namespace Application.DTO.LauncherDTO.LNC_004;

public class LNC_004 : IResponse
{
	public Project? Project { get; }

	public LNC_004(Project? project)
	{
		Project = project;
	}
}
