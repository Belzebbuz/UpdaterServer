using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ReleaseAssembly : BaseEntity
{
	public string Path { get; set; }
	[ForeignKey("PatchNoteId")]
	public List<PatchNote>? PatchNotes { get; set; }
	public Project Project { get; set; }
	public string Version { get; set; }
}
