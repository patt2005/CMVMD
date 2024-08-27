using CMVMD.Server.Services.User;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Persistance.Data;

namespace CMVMD.Server.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService, AppDbContext appDbContext)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> UserRegistration(UserRegistrationDto userRegistration)
    {
        var result = await _userService.RegisterNewUserAsync(userRegistration);
        if (result.IsUserRegistered)
        {
            return Ok(result.tokenDto);
        }

        ModelState.AddModelError("Email", "Failed to register the user!");
        return BadRequest(ModelState);
    }

    [HttpGet("validate/email")]
    public IActionResult CheckUniqueUserEmail(string email)
    {
        var result = _userService.CheckUserUniqueEmail(email);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto payload)
    {
        var result = await _userService.LoginAsync(payload);
        if (result.IsLoginSuccess)
        {
            return Ok(result.tokenResponseDto);
        }
        ModelState.AddModelError("LoginError", "Invalid Credentials");
        return BadRequest(ModelState);
    }
}
