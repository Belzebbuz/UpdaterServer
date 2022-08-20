using Microsoft.AspNetCore.Identity;

namespace Application.DTO.AuthDTO.ATH_400;

public class ATH_400 : IResponse
{
	public string ErrorName { get;}
	public List<ErrorData> Errors { get; }
	public ATH_400(IEnumerable<IdentityError> errors, string errorName)
	{
		ErrorName = errorName;
		Errors = new();
		foreach (var error in errors)
		{
			Errors.Add(new ErrorData(error.Code, error.Description));
		}
	}

	public ATH_400(string errorName)
	{
		ErrorName = errorName;
		Errors = new();
	}
}
