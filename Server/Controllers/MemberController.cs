using AutoMapper;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace CMVMD.Server.Controllers;

[ApiController]
[Route("api/members")]

public class MemberController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public MemberController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpGet("comision/component/getall")]
    public async Task<IActionResult> GetComponentMember()
    {
        var comision = await _appDbContext.ComisionComponentMembers.ToListAsync();
        var response = _mapper.Map<IEnumerable<ExecutiveMemberDto>>(comision);
        return Ok(response);
    }

    [HttpGet("dentistry/comision/getall")]
    public async Task<IActionResult> GetDentistryMembers()
    {
        var comision = await _appDbContext.DentistryComisionMembers.ToListAsync();
        var response = _mapper.Map<IEnumerable<ExecutiveMemberDto>>(comision);
        return Ok(response);
    }

    [HttpGet("executive/office/getall")]
    public async Task<IActionResult> GetExecutiveMembers()
    {
        var comision = await _appDbContext.ExecutiveOfficeMembers.ToListAsync();
        var response = _mapper.Map<IEnumerable<ExecutiveMemberDto>>(comision);
        return Ok(response);
    }

    [HttpGet("veterinarian/getall")]
    public async Task<IActionResult> GetVeterinarians()
    {
        var comision = await _appDbContext.Veterinarians.ToListAsync();
        var response = _mapper.Map<IEnumerable<VeterinarianDto>>(comision);
        return Ok(response);
    }
}
