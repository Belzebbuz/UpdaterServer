using Ardalis.Specification;
using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.AuthDTO.ATH_015
{
	public class ATH_015_Spec
	{
		public class FindRoleByIdSpec : Specification<AppUserRole>, ISingleResultSpecification
		{
			public FindRoleByIdSpec(string Id) => Query
				.Where(x => x.Id == Id)
				.Include(x => x.UserGroups)
				.ThenInclude(x => x.Users)
				.Include(x => x.UserRights);
		}
	}
}
