using System.ComponentModel.DataAnnotations;

namespace Persistance.Entities;

public class DocumentBase
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid FileId { get; set; }
    public File? File { get; set; }
}
