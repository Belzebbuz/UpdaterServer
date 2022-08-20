using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Project : BaseEntity
{
	public string Name { get; set; }
	public string Author { get; set; }
	public string Description { get; set; }
	public bool IsWinService { get; set; }
	public string ExeFile { get; set; }
	public string CurrentVersion { get; set; }

	[ForeignKey("ReleaseAssemblyId")]
	public List<ReleaseAssembly>? ReleaseAssemblies { get; set; }
}
