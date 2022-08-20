using Ardalis.Specification;
using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_017;

public class ATH_017_Spec
{
	public class FindRolesByUserIdSpec : Specification<AppUserRole>, ISingleResultSpecification
	{
		public FindRolesByUserIdSpec(string userId) =>
			Query
			.Where(x => x.UserGroups.Any(g => g.Users.Any(u => u.Id == userId)))
			.Include(x => x.UserRights);
	}
}
