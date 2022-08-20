using Ardalis.Specification;

namespace Application.DTO.LauncherDTO.LNC_005;

public class FindByIsServiceSpec : Specification<Project>, ISingleResultSpecification
{
	public FindByIsServiceSpec(bool isService) =>
			Query.Where(x => x.IsWinService == isService);
}