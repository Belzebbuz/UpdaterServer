using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Domain.Enties
{
	public class ProjectAssembly
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
		public DateTime ReleaseDate { get; set; } = DateTime.Now;
		
	}
}
