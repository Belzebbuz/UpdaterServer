using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Auth;
public class AppUser : IdentityUser, IAggregateRoot
{
	public List<UserGroup> UserGroups { get; set; }
}
