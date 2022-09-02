using Ardalis.Specification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.LauncherDTO.LNC_010
{
	public class LNC_010_Spec
	{
		public class FindByIdProjectSpec : Specification<Project>, ISingleResultSpecification
		{
			public FindByIdProjectSpec(Guid id) => 
				Query.Where(x => x.Id == id)
				.Include(x => x.ReleaseAssemblies)
				.ThenInclude(x => x.PatchNotes);
		}
	}
}
