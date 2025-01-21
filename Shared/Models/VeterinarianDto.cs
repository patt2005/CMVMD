namespace CMVMD.Shared.Models;

public class VeterinarianDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string VeterinarianCode { get; set; } = default!;
    public string DiplomaId { get; set; } = default!;
    private DateTime _registrationDate;

    public DateTime RegistrationDate
    {
        get => _registrationDate;
        set => _registrationDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    public bool IsActive { get; set; }
    public bool HasPenalties { get; set; }
}
