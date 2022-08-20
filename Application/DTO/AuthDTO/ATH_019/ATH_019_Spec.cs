using Ardalis.Specification;
using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_019;

public class ATH_019_Spec
{
	public class FindAllRolesSpec : Specification<AppUserRole>, ISingleResultSpecification
	{
		public FindAllRolesSpec() => 
			Query
			.Include(x => x.UserGroups)
			.Include(x => x.UserRights);
	}
}
