namespace Application.DTO.AuthDTO.ATH_400;

public class ErrorData
{
	public ErrorData(string errorCode, string errorDescription)
	{
		ErrorCode = errorCode;
		ErrorDescription = errorDescription;
	}

	public string ErrorCode { get; }
	public string ErrorDescription { get; }
}