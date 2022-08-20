using Ardalis.Specification;
using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.AuthDTO.ATH_010
{
	public class ATH_010_Spec 
	{
		public class FindGroupByIdSpec : Specification<UserGroup>, ISingleResultSpecification
		{
			public FindGroupByIdSpec(Guid id) =>
				Query.Where(x => x.Id == id).Include(x => x.Users);

		}
	}
}
