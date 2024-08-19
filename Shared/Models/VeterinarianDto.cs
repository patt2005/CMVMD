namespace CMVMD.Shared.Models;

public class VeterinarianDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string MyProperty { get; set; } = default!;
    public string VeterinarianCode { get; set; } = default!;
    public string DiplomaId { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }
    public bool HasPenalties { get; set; }
}
