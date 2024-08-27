using CMVMD.Shared.Models;

namespace CMVMD.Server.Services.User;

public interface IUserService
{
    Task<(bool IsUserRegistered, JWTTokenResponseDto tokenDto)> RegisterNewUserAsync(UserRegistrationDto userRegistration);

    bool CheckUserUniqueEmail(string email);

    Task<(bool IsLoginSuccess, JWTTokenResponseDto? tokenResponseDto)> LoginAsync(LoginDto loginPayload);
}
