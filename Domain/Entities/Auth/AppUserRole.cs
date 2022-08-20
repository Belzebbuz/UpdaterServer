using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Auth;

public class AppUserRole : IdentityRole, IAggregateRoot
{
	[ForeignKey("UserGroupId")]
	public List<UserGroup> UserGroups { get; set; }

	[ForeignKey("UserRightId")]
	public List<UserRight> UserRights { get; set; }
}