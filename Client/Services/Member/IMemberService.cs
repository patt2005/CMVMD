using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Member;

public interface IMemberService
{
    Task<IEnumerable<ExecutiveMemberDto>> GetComisionComponentMembers();
    Task<IEnumerable<ExecutiveMemberDto>> GetDentistryComisionMembers();
    Task<IEnumerable<ExecutiveMemberDto>> GetExecutiveOfficeMembers();
    Task<IEnumerable<VeterinarianDto>> GetVeterinarians();
    Task DeleteExecutiveOfficeMemberById(string id);
    Task DeleteDentistryComisionMemberById(string id);
    Task DeleteComisionComponentMemberById(string id);
    Task AddExecutiveOfficeMember(ExecutiveMemberDto member);
    Task AddDentistryComisionMember(ExecutiveMemberDto member);
    Task AddComisionComponentMember(ExecutiveMemberDto member);
}
