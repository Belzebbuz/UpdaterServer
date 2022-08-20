using Ardalis.Specification;
using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_009;

public class ATH_009_Spec
{
	public class FindUserGroupByNameSpec : Specification<UserGroup>, ISingleResultSpecification
	{
		public FindUserGroupByNameSpec(string name) => 
			Query.Where(x => x.Name == name);
	}
}
