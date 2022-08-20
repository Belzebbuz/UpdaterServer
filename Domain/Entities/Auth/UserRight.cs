using Domain.Entities.Common;

namespace Domain.Entities.Auth;

public class UserRight : IAggregateRoot
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public List<AppUserRole> UserRoles { get; set; }
}
