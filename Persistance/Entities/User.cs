using System.ComponentModel.DataAnnotations;

namespace Persistance.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
