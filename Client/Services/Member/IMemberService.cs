using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Member;

public interface IMemberService
{
    Task<IEnumerable<ExecutiveMemberDto>> GetComisionComponentMembers();
    Task<IEnumerable<ExecutiveMemberDto>> GetDentistryComisionMembers();
    Task<IEnumerable<ExecutiveMemberDto>> GetExecutiveOfficeMembers();
    Task<IEnumerable<VeterinarianDto>> GetVeterinarians();
}
