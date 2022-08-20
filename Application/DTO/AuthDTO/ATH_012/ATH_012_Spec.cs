using Ardalis.Specification;
using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_012;

public class ATH_012_Spec
{
	public class FindRoleByIdSpec : Specification<AppUserRole>, ISingleResultSpecification
	{
		public FindRoleByIdSpec(string id) => 
			Query
			.Where(x => x.Id == id)
			.Include(x => x.UserRights);
	}
}
