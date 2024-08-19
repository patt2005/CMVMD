using System.ComponentModel.DataAnnotations;

namespace Persistance.Entities;

public class Article
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string SubTitle { get; set; } = default!;
    public string Text { get; set; } = default!;
    public Guid FileId { get; set; } = default!;
    public File? File { get; set; }
}
