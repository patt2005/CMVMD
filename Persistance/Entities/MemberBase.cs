using System.ComponentModel.DataAnnotations;

namespace Persistance.Entities;

public class MemberBase
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string JobTitle { get; set; } = default!;
    public string? Contacts { get; set; }
}
