using AutoMapper;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Entities;

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

    [HttpPost("comision/component/add")]
    public async Task<IActionResult> AddComissionComomponentMember(ExecutiveMemberDto memberJson)
    {
        var member = _mapper.Map<ComisionComponentMember>(memberJson);
        await _appDbContext.ComisionComponentMembers.AddAsync(member);

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("dentistry/comision/add")]
    public async Task<IActionResult> AddDentistryComomponentMember(ExecutiveMemberDto memberJson)
    {
        var member = _mapper.Map<DentistryComisionMember>(memberJson);
        await _appDbContext.DentistryComisionMembers.AddAsync(member);

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("executive/office/add")]
    public async Task<IActionResult> AddExecutiveOfficeMember(ExecutiveMemberDto memberJson)
    {
        var member = _mapper.Map<ExecutiveOfficeMember>(memberJson);
        await _appDbContext.ExecutiveOfficeMembers.AddAsync(member);

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("executive/office/delete/{id}")]
    public async Task<IActionResult> DeleteExecutiveOfficeMember(string id)
    {
        var memeberId = Guid.Parse(id);
        var member = await _appDbContext.ExecutiveOfficeMembers.FirstAsync(a => a.Id == memeberId);

        if (member == null)
        {
            return NotFound();
        }

        _appDbContext.ExecutiveOfficeMembers.Remove(member);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("dentistry/comision/delete/{id}")]
    public async Task<IActionResult> DeleteDentistryComisionMember(string id)
    {
        var memeberId = Guid.Parse(id);
        var member = await _appDbContext.DentistryComisionMembers.FirstAsync(a => a.Id == memeberId);

        if (member == null)
        {
            return NotFound();
        }

        _appDbContext.DentistryComisionMembers.Remove(member);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("comision/component/delete/{id}")]
    public async Task<IActionResult> DeleteComisionComponentMember(string id)
    {
        var memeberId = Guid.Parse(id);
        var member = await _appDbContext.ComisionComponentMembers.FirstAsync(a => a.Id == memeberId);

        if (member == null)
        {
            return NotFound();
        }

        _appDbContext.ComisionComponentMembers.Remove(member);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("veterinarian/add")]
    public async Task<IActionResult> AddVeterinarian(VeterinarianDto memberJson)
    {
        var member = _mapper.Map<Veterinarian>(memberJson);
        await _appDbContext.Veterinarians.AddAsync(member);

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("veterinarian/delete/{id}")]
    public async Task<IActionResult> DeleteVeterinarian(string id)
    {
        var memeberId = Guid.Parse(id);
        var member = await _appDbContext.Veterinarians.FirstAsync(a => a.Id == memeberId);

        if (member == null)
        {
            return NotFound();
        }

        _appDbContext.Veterinarians.Remove(member);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("upload-file")]
    public async Task<IActionResult> UploadVeterinariansFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file provided.");
        }

        List<VeterinarianDto> veterinariansDto;
        using (var stream = new StreamReader(file.OpenReadStream()))
        {
            var content = await stream.ReadToEndAsync();
            try
            {
                veterinariansDto = System.Text.Json.JsonSerializer.Deserialize<List<VeterinarianDto>>(content);
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid JSON format: {ex.Message}");
            }
        }

        if (veterinariansDto == null || !veterinariansDto.Any())
        {
            return BadRequest("No data found in the JSON file.");
        }

        var veterinarians = veterinariansDto.Select(dto => new Veterinarian
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            VeterinarianCode = dto.VeterinarianCode,
            DiplomaId = dto.DiplomaId,
            RegistrationDate = dto.RegistrationDate,
            IsActive = dto.IsActive,
            HasPenalties = false // Default value
        }).ToList();

        await _appDbContext.Veterinarians.AddRangeAsync(veterinarians);
        await _appDbContext.SaveChangesAsync();

        return Ok(new { Message = "Veterinarians data uploaded successfully.", Count = veterinarians.Count });
    }
}