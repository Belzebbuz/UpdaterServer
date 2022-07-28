namespace UpdaterServer.Domain.Enties;

public class ReleaseAssembly : BaseEntity
{
	public Guid Id { get; set; }
	public string Path { get; set; }
	public DateTime ReleaseDate { get; set; }
	public string? PatchNote { get; set; }
	public virtual Project Project { get; set; }
	public string Version { get; set; }
}
