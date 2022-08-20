namespace Application.DTO.AuthDTO.ATH_011;

public class ATH_011 : IResponse
{
	public ATH_011(Guid id, IEnumerable<string> users)
	{
		Id = id;
		Users = users;
	}

	public Guid Id { get;}
	public IEnumerable<string> Users { get; }
}
