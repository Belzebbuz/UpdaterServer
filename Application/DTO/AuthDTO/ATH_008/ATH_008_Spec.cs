using Ardalis.Specification;
using Domain.Entities.Auth;

namespace Application.DTO.AuthDTO.ATH_008;

public class ATH_009_Spec
{
	public class FindUserRightByNameSpec : Specification<UserRight>, ISingleResultSpecification
	{
		public FindUserRightByNameSpec(string name) => 
			Query.Where(x => x.Name == name);
	}
}
