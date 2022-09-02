using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.LauncherDTO.LNC_012
{
	public class LNC_012_Spec
	{
		public class FindByIdRelease : Specification<ReleaseAssembly>, ISingleResultSpecification
		{
			public FindByIdRelease(Guid id) =>
				Query.Where(x => x.Id == id)
				.Include(x => x.PatchNotes);
		}
	}
}
