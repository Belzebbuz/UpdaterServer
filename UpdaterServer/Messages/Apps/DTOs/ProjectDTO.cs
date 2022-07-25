using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Messages.Apps.DTOs
{
	public class ProjectDTO
	{
		public ProjectDTO(Guid id, string name, string description, bool isWinService, string exeFile, List<Guid>? releaseAssemblies = null)
		{
			Id = id;
			Name = name;
			Description = description;
			IsWinService = isWinService;
			ExeFile = exeFile;
			ReleaseAssemblies = releaseAssemblies;
		}

		public Guid Id { get; }
		public string Name { get;}
		public string Description { get;}
		public List<Guid>? ReleaseAssemblies { get; set; }
		public bool IsWinService { get; set; }
		public string ExeFile { get; set; }

	}
}

