using System.Net.Http.Json;
using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Member;

public class MemberService : IMemberService
{
    private readonly HttpClient _httpClient;

    public MemberService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ExecutiveMemberDto>> GetComisionComponentMembers()
    {
        var httpResponse = await _httpClient.GetFromJsonAsync<IEnumerable<ExecutiveMemberDto>>("api/members/dentistry/comision/getall");
        return httpResponse!;
    }

    public async Task<IEnumerable<ExecutiveMemberDto>> GetDentistryComisionMembers()
    {
        var httpResponse = await _httpClient.GetFromJsonAsync<IEnumerable<ExecutiveMemberDto>>("api/members/comision/component/getall");
        return httpResponse!;
    }

    public async Task<IEnumerable<ExecutiveMemberDto>> GetExecutiveOfficeMembers()
    {
        var httpResponse = await _httpClient.GetFromJsonAsync<IEnumerable<ExecutiveMemberDto>>("api/members/executive/office/getall");
        return httpResponse!;
    }

    public async Task<IEnumerable<VeterinarianDto>> GetVeterinarians()
    {
        var httpResponse = await _httpClient.GetFromJsonAsync<IEnumerable<VeterinarianDto>>("api/members/veterinarian/getall");
        return httpResponse!;
    }
}
