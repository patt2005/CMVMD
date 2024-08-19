namespace CMVMD.Shared.Models;

public class ExecutiveMemberDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string JobTitle { get; set; } = default!;
    public string? Contacts { get; set; }
}
