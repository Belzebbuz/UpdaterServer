using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Domain.Enties
{
	public class Project : BaseEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }
		public bool IsWinService { get; set; }
		public string ExeFile { get; set; }

		[ForeignKey("ReleaseAssemblyId")]
		public virtual List<ReleaseAssembly>? ReleaseAssemblies { get; set; }
	}
}
