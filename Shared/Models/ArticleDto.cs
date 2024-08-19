namespace CMVMD.Shared.Models;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string SubTitle { get; set; } = default!;
    public string Text { get; set; } = default!;
    public Guid FileId { get; set; } = default!;
    public FileDto? File { get; set; }
}
