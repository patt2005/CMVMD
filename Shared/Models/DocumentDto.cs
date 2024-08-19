namespace CMVMD.Shared.Models;

public class DocumentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid FileId { get; set; }
    public FileDto? File { get; set; }
}
