namespace Application.DTO.AuthDTO.ATH_015;

public class ATH_015 : IRequest<IResponse>
{
	public Guid UserGroupId { get; set; }
	public string RoleId { get; set; }
}
