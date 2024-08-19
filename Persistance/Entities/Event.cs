using System.ComponentModel.DataAnnotations;

namespace Persistance.Entities;

public class Event
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string SubTitle { get; set; } = default!;
    public DateTime Date { get; set; }
    public Guid FileId { get; set; }
    public File? File { get; set; }
}
