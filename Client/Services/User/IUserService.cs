using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.User;

public interface IUserService
{
    Task<bool> Login(LoginDto loginDto);
    Task<bool> Register(UserRegistrationDto registrationDto);
    Task<bool> Logout();
}
