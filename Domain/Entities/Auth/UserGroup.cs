using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Auth;

public class UserGroup : IAggregateRoot
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public List<AppUser> Users { get; set; }
	public List<AppUserRole> Roles { get; set; }
}
