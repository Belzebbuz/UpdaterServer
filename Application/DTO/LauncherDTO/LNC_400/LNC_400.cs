using Application.DTO.AuthDTO.ATH_400;

namespace Application.DTO.LauncherDTO.LNC_400;

public class LNC_400 : IResponse
{
	public string ErrorName { get; }
	public List<ErrorData> Errors { get; }
	public LNC_400(string errorName)
	{
		ErrorName = errorName;
		Errors = new();
	}
}
