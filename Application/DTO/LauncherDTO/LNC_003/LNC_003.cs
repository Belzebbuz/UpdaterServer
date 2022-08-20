namespace Application.DTO.LauncherDTO.LNC_003;

public class LNC_003 : IResponse
{
    public List<Project> Projects { get; }

    public LNC_003(List<Project> projects)
    {
        Projects = projects;
    }
}
