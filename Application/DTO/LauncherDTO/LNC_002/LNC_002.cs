namespace Application.DTO.LauncherDTO.LNC_002;

public class LNC_002 : IResponse
{
	public Guid Id { get; }
	public LNC_002(Guid id)
	{
		Id = id;
	}
}
