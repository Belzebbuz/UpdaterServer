using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.LauncherDTO.LNC_001
{
	public class FindProjByNameSpec : Specification<Project>, ISingleResultSpecification
	{
		public FindProjByNameSpec(string name) =>
			Query.Where(x => x.Name == name);
	}
}
