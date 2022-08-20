namespace Application.DTO.AuthDTO.ATH_000;

public class ATH_000 : IResponse
{
	public int Status { get; }
	public ATH_000(int status)
	{
		Status = status;
	}
}
