namespace CMVMD.Shared.Models;

public class EventDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string SubTitle { get; set; } = default!;
    private DateTime _startDate;
    private DateTime _endDate;

    public DateTime StartDate
    {
        get => _startDate;
        set => _startDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); // ✅ Forțează UTC
    }
    public DateTime EndDate
    {
        get => _endDate;
        set => _endDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); // ✅ Forțează UTC
    }
    
    
    public Guid FileId { get; set; }
    public FileDto? File { get; set; }
}
