using System.ComponentModel.DataAnnotations;

namespace Persistance.Entities;

public class Veterinarian
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string VeterinarianCode { get; set; } = default!;
    public string DiplomaId { get; set; } = default!;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; }
    public bool HasPenalties { get; set; }
}
