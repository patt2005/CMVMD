using System.ComponentModel.DataAnnotations;

namespace Persistance.Entities;

public class FileBase
{
    [Key]
    public Guid Id { get; set; }
    public string FileName { get; set; } = default!;
    public string FileUrl { get; set; } = default!;
}
