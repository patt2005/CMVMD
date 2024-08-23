using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Member;

public class MemberService : IMemberService
{
    private readonly HttpClient _httpClient;

    public MemberService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddComisionComponentMember(ExecutiveMemberDto member)
    {
        await _httpClient.PostAsJsonAsync("api/members/comision/component/add", member);
    }

    public async Task AddDentistryComisionMember(ExecutiveMemberDto member)
    {
        await _httpClient.PostAsJsonAsync("api/members/dentistry/comision/add", member);
    }

    public async Task AddExecutiveOfficeMember(ExecutiveMemberDto member)
    {
        await _httpClient.PostAsJsonAsync("api/members/executive/office/add", member);
    }

    public async Task DeleteComisionComponentMemberById(string id)
    {
        await _httpClient.DeleteAsync($"api/members/comision/component/delete/{id}");
    }

    public async Task DeleteDentistryComisionMemberById(string id)
    {
        await _httpClient.DeleteAsync($"api/members/dentistry/comision/delete/{id}");
    }

    public async Task DeleteExecutiveOfficeMemberById(string id)
    {
        await _httpClient.DeleteAsync($"api/members/executive/office/delete/{id}");
    }

    public async Task<IEnumerable<ExecutiveMemberDto>> GetComisionComponentMembers()
    {
        var httpResponse = await _httpClient.GetFromJsonAsync<IEnumerable<ExecutiveMemberDto>>("api/members/comision/component/getall");
        return httpResponse!;
    }

    public async Task<IEnumerable<ExecutiveMemberDto>> GetDentistryComisionMembers()
    {
        var httpResponse = await _httpClient.GetFromJsonAsync<IEnumerable<ExecutiveMemberDto>>("api/members/dentistry/comision/getall");
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

    public async Task AddVeterinarian(VeterinarianDto veterinarianDto)
    {
        await _httpClient.PostAsJsonAsync("api/members/veterinarian/add", veterinarianDto);
    }

    public async Task DeleteVeterinarianById(string id)
    {
        await _httpClient.DeleteAsync($"api/members/veterinarian/delete/{id}");
    }
}
